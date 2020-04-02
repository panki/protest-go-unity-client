using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ProtestGoClient.Res
{
    [Serializable]
    class Error
    {
        public uint statusCode = 0;
        public string error = "";
        public string message = "";
    }

    [Serializable]
    public class Success
    {
        public bool success;
    }

    public enum UserAvatarStatus : uint
    {
        AVAILABLE = 1,
        PROTESTING = 2,
        ARRESTED = 3,
        HOSPITALOZED = 4,
    }

    public enum ProtestStatus : uint
    {
        ACTIVE = 1,
        LEAVED = 2,
        EXPIRED = 3,
        DISPERSED = 4,
    }

    public enum ParticipantStatus : uint
    {
        ACTIVE = 1,
        LEAVED = 2,
        ARRESTED = 3,
        KICKED = 4,
    }

    public enum LeafletStatus : uint
    {
        ACTIVE = 1,
        EXPIRED = 2,
        DESTROYED = 3,
    }

    public enum SignatoryStatus : uint
    {
        ACTIVE = 1,
        EXPIRED = 2,
        DESTROYED = 3,
    }

    public static class UserRole
    {
        public static string DEVELOPER = "developer";
    }

    public class Money
    {
        public uint amount;
        public string currency;
    }

}