using RSG;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ProtestGoClient
{

    namespace Res
    {
        [Serializable]
        public class Graph
        {
            public List<Protest> protests;
            public List<User> users;
            public List<UserAvatar> userAvatars;
            public List<Place> places;
            public List<Avatar> avatars;
        }
    }

    class GraphMap
    {
        private Dictionary<string, Res.Protest> protestsMap;
        private Dictionary<string, Res.User> usersMap;
        private Dictionary<string, Res.UserAvatar> userAvatarsMap;
        private Dictionary<uint, Res.Place> placesMap;
        private Dictionary<uint, Res.Avatar> avatarsMap;

        public GraphMap(Res.Graph g)
        {
            protestsMap = new Dictionary<string, Res.Protest>();
            g.protests.ForEach(p =>
            {
                protestsMap[p.id] = p;
            });

            usersMap = new Dictionary<string, Res.User>();
            g.users.ForEach(u =>
            {
                usersMap[u.id] = u;
            });

            userAvatarsMap = new Dictionary<string, Res.UserAvatar>();
            g.userAvatars.ForEach(a =>
            {
                userAvatarsMap[a.id] = a;
            });

            placesMap = new Dictionary<uint, Res.Place>();
            g.places.ForEach(p =>
            {
                placesMap[p.id] = p;
            });

            avatarsMap = new Dictionary<uint, Res.Avatar>();
            g.avatars.ForEach(a =>
            {
                avatarsMap[a.id] = a;
            });
            return;
        }

        public Res.Avatar Avatar(Res.Avatar avatar)
        {
            return avatar;
        }

        public List<Res.Avatar> Avatars(List<Res.Avatar> avatars)
        {
            return avatars;
        }

        public Res.Banner Banner(Res.Banner banner)
        {
            return banner;
        }

        public List<Res.Banner> Banners(List<Res.Banner> banners)
        {
            return banners;
        }

        public Res.User User(Res.User user)
        {
            fillUser(user);
            return user;
        }

        public List<Res.User> Users(List<Res.User> users)
        {
            fillUsers(users);
            return users;
        }

        public Res.Protest Protest(Res.Protest protest)
        {
            fillProtest(protest);
            return protest;
        }

        public List<Res.Protest> Protests(List<Res.Protest> protests)
        {
            fillProtests(protests);
            return protests;
        }

        public Res.Participant Participant(Res.Participant participant)
        {
            fillParticipant(participant);
            return participant;
        }

        public List<Res.Participant> Participants(List<Res.Participant> participants)
        {
            fillParticipants(participants);
            return participants;
        }

        private void fillUser(Res.User user)
        {
            fillUsersAvatars(user.userAvatars);
            fillParticipants(user.participations);
        }

        private void fillUsers(List<Res.User> users)
        {
            users.ForEach(u => fillUser(u));
        }

        private void fillUsersAvatar(Res.UserAvatar a)
        {
            if (a.avatar == null && avatarsMap.ContainsKey(a.avatarId))
                a.avatar = avatarsMap[a.avatarId];
        }

        private void fillUsersAvatars(List<Res.UserAvatar> userAvatars)
        {
            userAvatars.ForEach(a => fillUsersAvatar(a));
        }

        private void fillProtest(Res.Protest p)
        {
            if (p.place == null && placesMap.ContainsKey(p.placeId))
                p.place = placesMap[p.placeId];

        }

        private void fillProtests(List<Res.Protest> protests)
        {
            protests.ForEach(p => fillProtest(p));
        }

        private void fillParticipant(Res.Participant p)
        {
            if (p.protest == null && protestsMap.ContainsKey(p.protestId))
            {
                p.protest = protestsMap[p.protestId];
            }

            if (p.userAvatar == null && userAvatarsMap.ContainsKey(p.userAvatarId))
            {
                p.userAvatar = userAvatarsMap[p.userAvatarId];
            }
        }

        private void fillParticipants(List<Res.Participant> participants)
        {
            participants.ForEach(p => fillParticipant(p));
        }
    }
}