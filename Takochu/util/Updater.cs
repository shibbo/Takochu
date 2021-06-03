using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using Octokit;
using Takochu.ui;

namespace Takochu.util
{
    public class Updater
    {
        internal static string CompileDate
        {
            get => new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime.ToString();
        }

        static string ExeDir
        {
            get => new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName;
        }

        static List<Release> Releases = new List<Release>();

        static Release LatestRelease = null;

        static List<GitHubCommit> CommitList = new List<GitHubCommit>();

        static DateTimeOffset CurrentRelease;

        internal static GitHubClient Client
        {
            get => new GitHubClient(new ProductHeaderValue("Takochu_Updater"));
        }

        public static void Update(bool IsBleedingEdge)
        {
            Client.GetReleases(ref Releases, user: SettingsForm.User);
            Client.GetCommits(CompileDate, ref CurrentRelease, ref CommitList, user: SettingsForm.User);

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
                        continue;
            }
            if (LatestRelease is null)
            {
                if (IsBleedingEdge)
                    MessageBox.Show("Failed to find any Release marked as a Prerelease. Try again with the box not checked.");
                else
                    MessageBox.Show("Failed to find any Release not marked as a Prerelease. Try again with the box checked.");
                return;
            }
            if (LatestRelease.Assets[0].UpdatedAt > CurrentRelease)
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
            var start = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = $"/c curl -k -L {url} -o {filename}",
                CreateNoWindow = true
            };

            Environment.CurrentDirectory = ExeDir;

            var p = Process.Start(start);

            while (!p.HasExited)
                if (p.HasExited)
                    break;
        }
    }

    internal static class OctoExtensions
    {
        internal static void GetReleases(this GitHubClient client, ref List<Release> releases, string user = "shibbo", string repo = "Takochu")
        {
            releases = new List<Release>();
            foreach (var r in client.Repository.Release.GetAll(user, repo).GetAwaiter().GetResult())
            {
                releases.Add(r);
            }
        }

        internal static void GetCommits(this GitHubClient client, string time, ref DateTimeOffset offset, ref List<GitHubCommit> commits, string user = "shibbo", string repo = "Takochu")
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
