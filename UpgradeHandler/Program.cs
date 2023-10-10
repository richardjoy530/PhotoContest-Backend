using System.Data;
using System.Data.SqlClient;
using System.Net;
using PhotoContest.Models;
using FileInfo = PhotoContest.Models.FileInfo;

// ReSharper disable PossibleMultipleEnumeration

namespace UpgradeHandler
{
    internal abstract class Program
    {
        private static readonly string _connectionString =
            "Server=localhost;Database=FridayDatabase;Trusted_Connection=yes;";

        public static IEnumerable<Contest> allContests { get; set; }
        public static IEnumerable<FileInfo> allFileInfos { get; set; }
        public static IEnumerable<UserInfo> allUserInfos { get; set; }
        public static IEnumerable<Submission> allSubmissionDatas { get; set; }

        public static void Main()
        {
            // allContests = GetAllContest();
            allFileInfos = GetAllFileInfos();
            // allUserInfos = GetAllUserInfos();
            // allSubmissionDatas = GetAllSubmissionData();

            // foreach (var entrusting in File.ReadAllLines(@"E:\Project\Python\data1.log"))
            // {
            //     var tokens = entrusting.Split("##");
            //     var userRefId = tokens[0].Trim();
            //     var userName = tokens[1].Trim();
            //     var caption = tokens[2].Trim();
            //     var uploadTimeEpoc = long.Parse(tokens[3].Trim());
            //     DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(uploadTimeEpoc);
            //     var score = tokens[4].Trim();
            //     var url = tokens[5].Trim();
            //     var entryRefId = tokens[6].Trim().Split('-')[0];
            //     var theme = tokens[6].Trim().Split('-')[1];
            //
            //     if (allUserInfos.Select(u=>u.Name).Contains(userName))
            //     {
            //         var userInfo = allUserInfos.First(u => u.Name == userName);
            //         if (userInfo.RefId is null or " ")
            //         {
            //             userInfo.RefId = userRefId;
            //             UpdateUserInfo(userInfo, userInfo.Id);
            //         }
            //         else if (userInfo.RefId != userRefId)
            //         {
            //             Console.WriteLine($"{userName} has multiple ref id {userRefId} and {userInfo.RefId}");
            //         }
            //     }
            //     else
            //     {
            //         Console.WriteLine(entrusting);
            //     }
            //
            //     var submission = new Submission()
            //     {
            //         UserInfo = allUserInfos.First(u => u.Name == userName),
            //         Caption = caption,
            //         UploadedOn = dateTimeOffset.ToLocalTime().DateTime,
            //         FileInfo = allFileInfos.First(f => f.Path == url),
            //         Contest = allContests.First(c => c.Theme == theme),
            //         RefId = entryRefId
            //     };
            //
            //     InsertSubmission(submission);
            // }

            // foreach (var voteInfoString in File.ReadAllLines(@"E:\Project\Python\vote.log"))
            // {
            //     var tokens = voteInfoString.Split('#');
            //     var theme = tokens[0].Trim();
            //     var userId = tokens[1].Trim();
            //     var first = tokens[2].Trim();
            //     var second = tokens[3].Trim();
            //     var third = tokens[4].Trim();
            //
            //     // if (first !="" && !allSubmissionDatas.Select(u => u.RefId).Contains(first))
            //     //     Console.WriteLine($"{first} not present in the submission db theme {theme}");
            //     // if (second !="" && !allSubmissionDatas.Select(u => u.RefId).Contains(second))
            //     //     Console.WriteLine($"{second} not present in the submission db theme {theme}");
            //     // if (third !="" && !allSubmissionDatas.Select(u => u.RefId).Contains(third))
            //     //     Console.WriteLine($"{third} not present in the submission db theme {theme}");
            //     
            //     var voteInfo = new VoteInfoData()
            //     {
            //         ContestId = allContests.First(c => c.Theme == theme).Id,
            //         UserId = allUserInfos.First((u => u.RefId == userId)).Id,
            //         FirstId = first == "" ? 0 : allSubmissionDatas.First(s => s.RefId == first).Id,
            //         SecondId = second == "" ? 0 : allSubmissionDatas.First(s => s.RefId == second).Id,
            //         ThirdId = third == "" ? 0 : allSubmissionDatas.First(s => s.RefId == third).Id
            //     };
            //     InsertVoteInfoData(voteInfo);
            // }

            // foreach (var entrusting in File.ReadAllLines(@"E:\Project\Python\auth.html"))
            // {
            //     var tokens = entrusting.Split("#");
            //     var userRefId = tokens[0].Trim();
            //     var email = tokens[1].Trim();
            //     var regDateString = tokens[2].Trim();
            //     var regDateTime = DateTime.Parse(regDateString);
            //
            //     if (allUserInfos.Select(u=>u.RefId).Contains(userRefId))
            //     {
            //         var userInfo = allUserInfos.First(u => u.RefId == userRefId);
            //         if (userInfo.RegisteredDate == default)
            //         {
            //             userInfo.Email = email;
            //             userInfo.RegisteredDate = regDateTime;
            //             UpdateUserInfo(userInfo, userInfo.Id);
            //         }
            //     }
            //     else
            //     {
            //         var userInfo = new UserInfo()
            //         {
            //             Name = $"Disabled {email}",
            //             RefId = userRefId,
            //             Email = email,
            //             RegisteredDate = regDateTime,
            //         };
            //         if (!allUserInfos.Select(u => u.RefId).Contains(userRefId))
            //         {
            //             InsertUserInfo(userInfo);
            //             allUserInfos = GetAllUserInfos();
            //         }
            //     }
            // }

            foreach (var info in allFileInfos)
            {
                if (info.Id == 0)
                {
                    continue;
                }
                using (var client = new WebClient()) 
                {
                    // client.DownloadFile(new Uri(info.Path), @$"images\{info.RefId}.png");
                }
            }
        }

        public static void UpdateContest(Contest data, int id)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[Contest_Update]";
            command.Parameters.Add(new SqlParameter("@Id", id));
            command.Parameters.Add(new SqlParameter("@Theme", data.Theme));
            command.Parameters.Add(new SqlParameter("@EndDate", data.EndDate));
            command.Parameters.Add(new SqlParameter("@updateEndDate", true));
            command.ExecuteNonQuery();
        }

        public static int InsertVoteInfoData(VoteInfoData data)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[VoteInfo_Insert]";
            command.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
            command.Parameters.Add(new SqlParameter("@ContestId", data.ContestId));
            command.Parameters.Add(new SqlParameter("@FirstId", data.FirstId));
            command.Parameters.Add(new SqlParameter("@SecondId", data.SecondId));
            command.Parameters.Add(new SqlParameter("@ThirdId", data.ThirdId));
            command.Parameters.Add(new SqlParameter("@UserId", data.UserId));
            command.ExecuteNonQuery();
            return Convert.ToInt32(command.Parameters["@Id"].Value);
        }

        public static int InsertUserInfo(UserInfo data)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[UserInfo_Insert]";
            command.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
            command.Parameters.Add(new SqlParameter("@Name", data.Name));
            command.Parameters.Add(new SqlParameter("@Email", data.Email));
            command.Parameters.Add(new SqlParameter("@RefId", data.RefId));
            command.Parameters.Add(new SqlParameter("@RegisteredDate", data.RegisteredDate));
            command.ExecuteNonQuery();

            return Convert.ToInt32(command.Parameters["@Id"].Value);
        }

        public static void UpdateUserInfo(UserInfo data, int id)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[UserInfo_Update]";
            command.Parameters.Add(new SqlParameter("@Id", id));
            command.Parameters.Add(new SqlParameter("@Name", data.Name));
            command.Parameters.Add(new SqlParameter("@Email", data.Email));
            command.Parameters.Add(new SqlParameter("@RefId", data.RefId));
            command.Parameters.Add(new SqlParameter("@RegisteredDate", data.RegisteredDate));
            command.Parameters.Add(new SqlParameter("@UpdateName", false));
            command.Parameters.Add(new SqlParameter("@UpdateEmail", false));
            command.Parameters.Add(new SqlParameter("@UpdateRefId", false));
            command.Parameters.Add(new SqlParameter("@UpdateRegisteredDate", true));
            command.ExecuteNonQuery();
        }

        public static int InsertSubmission(Submission data)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[Submission_Insert]";
            command.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
            command.Parameters.Add(new SqlParameter("@ContestId", data.Contest.Id));
            command.Parameters.Add(new SqlParameter("@FileInfoId", data.FileInfo.Id));
            command.Parameters.Add(new SqlParameter("@Caption", data.Caption));
            command.Parameters.Add(new SqlParameter("@UserId", data.UserInfo.Id));
            command.Parameters.Add(new SqlParameter("@RefId", data.RefId));
            command.Parameters.Add(new SqlParameter("@UploadedOn", data.UploadedOn));
            command.ExecuteNonQuery();
            return Convert.ToInt32(command.Parameters["@Id"].Value);
        }

        private static IEnumerable<Contest> GetAllContest()
        {
            var contests = new List<Contest>();
            using SqlConnection connection = new(_connectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[Contest_GetAll]";
            using var reader = command.ExecuteReader();
            while (reader.Read())
                contests.Add(ParseData(reader));

            return contests;
        }

        public static IEnumerable<FileInfo> GetAllFileInfos()
        {
            var contests = new List<FileInfo>();
            using SqlConnection connection = new(_connectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[FileInfo_GetAll]";
            using var reader = command.ExecuteReader();
            while (reader.Read())
                contests.Add(ParseDataFileInfo(reader));

            return contests;
        }

        public static IEnumerable<Submission> GetAllSubmissionData()
        {
            var contests = new List<Submission>();
            using SqlConnection connection = new(_connectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[Submission_GetAll]";
            using var reader = command.ExecuteReader();
            while (reader.Read())
                contests.Add(ParseDataSubmission(reader));

            return contests;
        }

        public static IEnumerable<UserInfo> GetAllUserInfos()
        {
            var contests = new List<UserInfo>();
            using SqlConnection connection = new(_connectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[UserInfo_GetAll]";
            using var reader = command.ExecuteReader();
            while (reader.Read())
                contests.Add(ParseDataUserInfo(reader));

            return contests;
        }

        private static UserInfo ParseDataUserInfo(IDataRecord record)
        {
            if (record is null)
                throw new ArgumentNullException(nameof(record));

            return new UserInfo
            {
                Name = (string)record["Name"],
                Id = (int)record["Id"],
                RefId = (string)record["RefId"],
                RegisteredDate = (DateTime)record["RegisteredDate"],
                Email = (string)record["Email"]
            };
        }

        private static Contest ParseData(IDataRecord record)
        {
            if (record is null)
                throw new ArgumentNullException(nameof(record));

            return new Contest
            {
                EndDate = (DateTime)record["EndDate"],
                Theme = (string)record["Theme"],
                Id = (int)record["Id"]
            };
        }

        private static FileInfo ParseDataFileInfo(IDataRecord record)
        {
            if (record is null)
                throw new ArgumentNullException(nameof(record));

            return new FileInfo
            {
                Path = (string)record["Path"],
                Id = (int)record["Id"],
                RefId = (string)record["RefId"]
            };
        }

        private static Submission ParseDataSubmission(IDataRecord record)
        {
            if (record is null)
                throw new ArgumentNullException(nameof(record));

            return new Submission
            {
                Id = (int)record["Id"],
                Caption = (string)record["Caption"],
                RefId = (string)record["RefId"],
                UserInfo = allUserInfos.First(u => u.Id == (int)record["UserId"]),
                FileInfo = allFileInfos.First(f => f.Id == (int)record["FileInfoId"]),
                Contest = allContests.First(c => c.Id == (int)record["ContestId"]),
                UploadedOn = (DateTime)record["UploadedOn"]
            };
        }
    }

    public record SubmissionData
    {
        /// <summary>
        /// </summary>
        public string Caption;

        /// <summary>
        /// </summary>
        public int ContestId;

        /// <summary>
        /// </summary>
        public int FileInfoId;

        /// <summary>
        /// </summary>
        public int Id;

        /// <summary>
        /// </summary>
        public string RefId;

        /// <summary>
        /// </summary>
        public DateTime UploadedOn;

        /// <summary>
        /// </summary>
        public int UserId;
    }

    public record VoteInfoData
    {
        /// <summary>
        /// </summary>
        public int ContestId;

        /// <summary>
        /// </summary>
        public int FirstId;

        /// <summary>
        /// </summary>
        public int Id;

        /// <summary>
        /// </summary>
        public int SecondId;

        /// <summary>
        /// </summary>
        public int ThirdId;

        /// <summary>
        /// </summary>
        public int UserId;
    }
}