using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Firebase.Database;

namespace Persistence
{
    public class PersistenceManager : Singleton<PersistenceManager>
    {
        private DatabaseReference databaseReference;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }

        // Start is called before the first frame update
        void Start()
        {
            // Set up the database reference
            databaseReference = FirebaseDatabase.DefaultInstance.GetReference("users");
        }

        public void SaveGame()
        {
            StartCoroutine(SavePlayerData());
        }

        public void LoadGame(string userUID)
        {
            StartCoroutine(LoadPlayerData(userUID));
        }

        IEnumerator SavePlayerData()
        {
            // Convert the PlayerData object to a JSON string
            string json = JsonConvert.SerializeObject(Player.Instance.playerData);

            // Save the JSON string to the database
            Task task = databaseReference.Child(Player.Instance.playerData.UserUID).SetRawJsonValueAsync(json);

            // Wait for the database operation to complete
            yield return new WaitUntil(() => task.IsCompleted);

            // Check if the operation was successful
            if (task.Exception != null)
            {
                yield break;
            }
        }

        IEnumerator LoadPlayerData(string userUID)
        {
            // Get the player data in json string from the Realtime Database
            Task<DataSnapshot> task = databaseReference.Child(userUID).GetValueAsync();

            // Wait for the database operation to complete
            yield return new WaitUntil(() => task.IsCompleted);

            // Check if the operation was successful
            if (task.Exception != null)
            {
                yield break;
            }

            DataSnapshot snapshot = task.Result;

            // Check if the result of the operation is null (no data was found)
            if (snapshot.Value != null)
            {
                // Deserialize the snapshot's value as a PlayerData object using JsonConvert
                string json = snapshot.GetRawJsonValue();

                Player.Instance.playerData = JsonConvert.DeserializeObject<PlayerData>(json);
            }
            else
            {
                // Create new player data if there is none (user doesn't have player data in Realtime Database)
                string startingUsername = "New Player";
                int startingPlasmids = 100;
                int startingTickets = 1;
                Dictionary<string, bool> subjectsTest = new Dictionary<string, bool>
                {
                    {"A001", true},
                    {"D001", true},
                    {"S001", true}
                };
                PlayerData playerData = new(userUID, startingUsername, startingPlasmids, startingTickets, subjectsTest);
                Player.Instance.playerData = playerData;

                StartCoroutine(SavePlayerData());
            }

            // load to Menu Scene
            LoadingManager.Instance.LoadScene((int)SceneIndices.Menu);
        }
    }
}
