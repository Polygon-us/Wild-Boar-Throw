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
            Debug.Log($"Posted {(long)distance} to leaderboard");
            PlayGamesPlatform.Instance.ReportScore((long)distance, id.leaderboard_throw, OnPostLeaderboard);
        }

        private static void OnPostLeaderboard(bool result)
        {
            Debug.Log($"Leaderboard Posted: {result}");
        }
    }
}