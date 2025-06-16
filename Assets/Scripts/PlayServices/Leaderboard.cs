using GooglePlayGames;
using UnityEngine;

namespace PlayServices
{
    public static class Leaderboard
    {
        public static void ShowLeaderboard()
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI(id.leaderboard_distance);
        }

        public static void PostLeaderboard(float distance)
        {
            Debug.Log("Post leaderboard");
            PlayGamesPlatform.Instance.ReportScore((long)distance, id.leaderboard_distance, OnPostLeaderboard);
        }

        private static void OnPostLeaderboard(bool result)
        {
            Debug.Log($"Leaderboard Posted: {result}");
        }
    }
}