﻿using System;

namespace Constants {
    // Json Serialisation classes here
    public class JsonPort {
        public int port;
    }

    public class Room {
        public int room;
    }
    public class AuthData {
        public string password;
        public string username;
    }

    public class Error {
        public string error;
    }

    public class User {
        public bool isGuest;
        public string username;
        public int id;
        public string token;
        public int score;        
        public ItemsInInventory[] itemsInInventory;
    }

    [Serializable] public class ItemsInInventory {
        public string id;
        public string chargesLeft;
    }

    public class Token {
        public string token;
    }
}