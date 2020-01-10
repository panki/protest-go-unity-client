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
            public List<Leaflet> leaflets;
            public List<User> users;
            public List<UserAvatar> userAvatars;
            public List<Place> places;
            public List<Avatar> avatars;
        }
    }

    class GraphMap
    {
        private Dictionary<string, Res.Protest> protestsMap;
        private Dictionary<string, Res.Leaflet> leafletsMap;
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

            leafletsMap = new Dictionary<string, Res.Leaflet>();
            g.leaflets.ForEach(l =>
            {
                leafletsMap[l.id] = l;
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

        public Res.Leaflet Leaflet(Res.Leaflet leaflet)
        {
            fillLeaflet(leaflet);
            return leaflet;
        }

        public List<Res.Leaflet> Leaflets(List<Res.Leaflet> leaflets)
        {
            fillLeaflets(leaflets);
            return leaflets;
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

        public Res.Signatory Signatory(Res.Signatory signatory)
        {
            fillSignatory(signatory);
            return signatory;
        }

        public List<Res.Signatory> Signatories(List<Res.Signatory> signatories)
        {
            fillSignatories(signatories);
            return signatories;
        }

        public Res.Place Place(Res.Place place)
        {
            fillPlace(place);
            return place;
        }

        public List<Res.Place> Places(List<Res.Place> places)
        {
            fillPlaces(places);
            return places;
        }

        private void fillUser(Res.User user)
        {
            fillUsersAvatars(user.userAvatars);
            fillParticipants(user.participations);
            fillSignatories(user.signatures);
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
            if (p.organizer == null && usersMap.ContainsKey(p.organizerId))
                p.organizer = usersMap[p.organizerId];

        }

        private void fillProtests(List<Res.Protest> protests)
        {
            protests.ForEach(p => fillProtest(p));
        }

        private void fillLeaflet(Res.Leaflet l)
        {
            if (l.place == null && placesMap.ContainsKey(l.placeId))
                l.place = placesMap[l.placeId];

            if (l.organizer == null && usersMap.ContainsKey(l.organizerId))
                l.organizer = usersMap[l.organizerId];
        }

        private void fillLeaflets(List<Res.Leaflet> leaflets)
        {
            leaflets.ForEach(l => fillLeaflet(l));
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

        private void fillSignatory(Res.Signatory s)
        {
            if (s.leaflet == null && leafletsMap.ContainsKey(s.leafletId))
            {
                s.leaflet = leafletsMap[s.leafletId];
            }
            if (s.user == null && usersMap.ContainsKey(s.userId))
            {
                s.user = usersMap[s.userId];
            }
        }

        private void fillSignatories(List<Res.Signatory> signatories)
        {
            signatories.ForEach(s => fillSignatory(s));
        }

        private void fillPlace(Res.Place p)
        {
            if (p.protest == null && protestsMap.ContainsKey(p.protestId))
            {
                p.protest = protestsMap[p.protestId];
            }
            if (p.leaflet == null && leafletsMap.ContainsKey(p.leafletId))
            {
                p.leaflet = leafletsMap[p.leafletId];
            }
        }

        private void fillPlaces(List<Res.Place> places)
        {
            places.ForEach(p => fillPlace(p));
        }
    }
}