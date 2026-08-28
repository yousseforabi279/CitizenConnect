namespace DeputyProject.Common
{
    public class ApiRoutes
    {
        public static class Complaint
        {
            public const string CreateComplaint = "api/auth/Complaint";
        }

        public static class Authentication
        {
            public const string Login = "api/auth/Login";
            public const string Register = "api/auth/Register";
        }
        public static class Deputy
        {
            public const string Edit = "api/HeroInfo";
            public const string GetDeputy = "api/HeroInfo";
        
        }
        public static class Achievements
        {
            public const string GetAllAchievements = "/api/achievements";
            public const string GetAchievementById = "/api/achievements/{achievementId:int}";
            public const string CreateAchievement = "/api/achievements";
            public const string EditAchivement = "/api/achievements/{AchievementId:int}";
            public const string DeleteAchievements = "/api/achievements/{AchievementId:int}";
        }
        public static class ActivitiesVisits
        {
            public const string POST = "/api/activities-visits";
            public const string GETALL = "/api/activities-visits";
            public const string GETBYID = "/api/activities-visits/{ActivityVisitId}";
            public const string PUT = "/api/activities-visits/{ActivityVisitId}";
            public const string DELETE = "/api/activities-visits/{ActivityVisitId}";
        }
        public static class AreaOfWork
        {
            public const string POST = "/api/areas-of-work";
            public const string GETALL = "/api/areas-of-work";
            public const string GETBYID = "/api/areas-of-work/{areaId}";
            public const string PUT = "/api/areas-of-work/{areaId}";
            public const string DELETE = "/api/areas-of-work/{reaId}";
        }

    }
}
