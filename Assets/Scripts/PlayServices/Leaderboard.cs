using GooglePlayGames;
using UnityEngine;

namespace PlayServices
{
    public static class Leaderboard
    {
        public static void ShowLeaderboard()
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI(id.leaderboard_throw);
        }

        public static void PostLeaderboard(float distance)
        {
            long score = (long)(distance * 10);
            Debug.Log($"Posted {(long)distance} to leaderboard");
            PlayGamesPlatform.Instance.ReportScore(score, id.leaderboard_throw, OnPostLeaderboard);
        }

        private static void OnPostLeaderboard(bool result)
        {
            Debug.Log($"Leaderboard Posted: {result}");
        }
    }
}