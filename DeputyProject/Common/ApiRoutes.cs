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
            public const string CreateAchievement = "api/achievements";
            public const string EditAchivement = "api/achievements/{achievementId}";
            public const string GetAchievementById = "api/achievements/{achievementId:int}/{deputyId:int}";
            public const string GetAllAchievements = "api/achievements/deputy/{deputyId:int}";
            public const string DeleteAchievements = "api/achievements/{achievementId:int}/{deputyId:int}";
        }
        public static class ActivitiesVisits
        {
            public const string POST = "/api/deputies/{deputyId}/activities-visits";
            public const string GETALL = "/api/deputies/{deputyId}/activities-visits";
            public const string GETBYID = "/api/deputies/{deputyId}/activities-visits/{id}";
            public const string PUT = "/api/deputies/{deputyId}/activities-visits/{id}";
            public const string DELETE = "/api/deputies/{deputyId}/activities-visits/{id}";
        }
        public static class AreaOfWork
        {
            public const string POST = "/api/deputies/{deputyId:int}/areas-of-work";
            public const string GETALL = "/api/deputies/{deputyId}/areas-of-work";
            public const string GETBYID = "/api/deputies/{deputyId}/areas-of-work/{areaId}";
            public const string PUT = "/api/deputies/{deputyId}/areas-of-work/{areaId}";
            public const string DELETE = "/api/deputies/{deputyId}/areas-of-work/{areaId}";
        }

    }
}
