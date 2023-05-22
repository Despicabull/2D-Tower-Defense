using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace Persistence
{
    [Serializable]
    public class PlayerData
    {
        private string userUID;
        private string username;
        private int plasmids;
        private int tickets;
        private Dictionary<string, bool> subjects;

        public string UserUID
        {
            get
            {
                return userUID;
            }
            set
            {
                userUID = value;
                PersistenceManager.Instance.SaveGame();
            }
        }

        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                PersistenceManager.Instance.SaveGame();
            }
        }

        public int Plasmids
        {
            get
            {
                return plasmids;
            }
            set
            {
                plasmids = value;
                PersistenceManager.Instance.SaveGame();
            }
        }

        public int Tickets
        {
            get
            {
                return tickets;
            }
            set
            {
                tickets = value;
                PersistenceManager.Instance.SaveGame();
            }
        }

        public Dictionary<string, bool> Subjects
        {
            get
            {
                return subjects;
            }
            set
            {
                subjects = value;
                PersistenceManager.Instance.SaveGame();
            }
        }

        [JsonConstructor]
        public PlayerData(string userUID, string username, int plasmids, int tickets, Dictionary<string, bool> subjects)
        {
            this.userUID = userUID;
            this.username = username;
            this.plasmids = plasmids;
            this.tickets = tickets;
            this.subjects = subjects;
        }
    }
}