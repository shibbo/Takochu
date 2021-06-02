using System;
using System.Net;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using Octokit;

namespace Takochu.util
{
    public class Updater
    {
        static string CompileDate
        {
            get => new FileInfo(Assembly.GetExecutingAssembly().Location).CreationTime.ToString();
        }

        static List<Release> Releases = new List<Release>();

        static Release LatestRelease;

        static List<GitHubCommit> CommitList = new List<GitHubCommit>();

        static DateTimeOffset CurrentRelease;

        static GitHubClient Client
        {
            get => new GitHubClient(new ProductHeaderValue("Takochu_Updater"));
        }

        public static void Update(bool IsBleedingEdge)
        {
            Client.GetReleases(ref Releases);
            Client.GetCommits(CompileDate, ref CurrentRelease, ref CommitList);

            if (IsBleedingEdge)
            {
                foreach (var r in Releases)
                    if (r.Prerelease)
                    {
                        LatestRelease = r;
                        break;
                    }
                    else
                        continue;
            }
            else
            {
                foreach (var r in Releases)
                    if (!r.Prerelease)
                    {
                        LatestRelease = r;
                        break;
                    }
                    else
                        break;
            }

            if (LatestRelease.Assets[0].CreatedAt.DateTime < CurrentRelease.DateTime)
            {
                Download(LatestRelease.Assets[0].BrowserDownloadUrl, LatestRelease.Assets[0].Name);
                MessageBox.Show("Release zip downloaded. You'll have to unzip it yourself.");
            } 
            else
            {
                MessageBox.Show("No need to update!");
            }
        }

        private static void Download(string url, string filename)
        {
            var client = new WebClient();
            client.DownloadFile(url, filename);
        }
    }

    internal static class OctoExtensions
    {
        internal static void GetReleases(this GitHubClient client, ref List<Release> releases, string user = "shibbs", string repo = "Takochu")
        {
            releases = new List<Release>();
            foreach (var r in client.Repository.Release.GetAll(user, repo).GetAwaiter().GetResult())
            {
                releases.Add(r);
            }
        }

        internal static void GetCommits(this GitHubClient client, string time, ref DateTimeOffset offset, ref List<GitHubCommit> commits, string user = "shibbs", string repo = "Takochu")
        {
            var IsValid = DateTimeOffset.TryParse(time, out offset);

            foreach (var c in client.Repository.Commit.GetAll(user,repo).GetAwaiter().GetResult())
            {
                if (IsValid)
                    if (offset.DateTime < c.Commit.Author.Date.DateTime)
                        commits.Add(c);
                    else
                        break;
                else
                    commits.Add(c);
            }
        }
    }
}
