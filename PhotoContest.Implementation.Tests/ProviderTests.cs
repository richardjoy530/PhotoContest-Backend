// using System.Data;
// using System.Data.SqlClient;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Logging;
// using Microsoft.Extensions.Logging.Abstractions;
// using PhotoContest.Implementation.Ado.DataRecords;
// using PhotoContest.Models;
// using Contest = PhotoContest.Implementation.Ado.DataRecords.Contest;
// using FileInfo = PhotoContest.Implementation.Ado.DataRecords.FileInfo;
// using Submission = PhotoContest.Implementation.Ado.DataRecords.Submission;
// using UserInfo = PhotoContest.Implementation.Ado.DataRecords.UserInfo;
//
// namespace PhotoContest.Implementation.Tests;
//
// public class ProviderTests
// {
//     private static readonly string ConnectionString = "Server=localhost;Database=TestDatabase;Trusted_Connection=yes;";
//     private IDataStore CachedDataStore { get; set; }
//     private static IList<KeyValuePair<AssetType, int>> IdentityMap { get; set; }
//
//     [OneTimeSetUp]
//     public void OneTimeSetUp()
//     {
//         var serviceCollection = new ServiceCollection();
//         serviceCollection.AddSingleton<IDbConnection>(_ => new SqlConnection(ConnectionString));
//         serviceCollection.AddSingleton(typeof(ILogger), new NullLogger<ProviderTests>());
//         serviceCollection.ConfigureServices(true);
//
//         var serviceProvider = serviceCollection.BuildServiceProvider();
//         CachedDataStore = serviceProvider.GetService<IDataStore>() ?? throw new InvalidOperationException();
//         IdentityMap = new List<KeyValuePair<AssetType, int>>();
//     }
//
//     [TestCase("Sceen Chako", "sceen.chako@gmail.com")]
//     [TestCase("Chako Fernandus", "chucki.chako@gmail.com")]
//     public void UserProviderTests(string name, string email)
//     {
//         var userInfo = new UserInfo
//         {
//             Name = name,
//             Email = email,
//             RegistrationDate = DateTime.Today,
//             RefId = Guid.NewGuid().ToString()
//         };
//
//         //Insert
//         CachedDataStore.Insert(userInfo, AssetType.UserInfo);
//
//         //Get by id
//         UserInfo EnsureInfoById(UserInfo expected)
//         {
//             var actual = CachedDataStore.Get(expected.Id, AssetType.UserInfo) as UserInfo;
//             Assert.That(actual.Id, Is.EqualTo(expected.Id));
//             Assert.That(actual.Name, Is.EqualTo(expected.Name));
//             Assert.That(actual.Email, Is.EqualTo(expected.Email));
//             Assert.That(actual.RegistrationDate, Is.EqualTo(expected.RegistrationDate));
//             Assert.That(actual.RefId, Is.EqualTo(expected.RefId));
//             return actual;
//         }
//
//         EnsureInfoById(userInfo);
//
//         //Update
//         userInfo.Email = $"updated_{email}";
//         userInfo.Name = $"updated_{name}";
//         var updateParams = UserInfoParams.Email | UserInfoParams.Name;
//         CachedDataStore.Update(userInfo, AssetType.UserInfo, (long)updateParams);
//         EnsureInfoById(userInfo);
//
//         IdentityMap.Add(new KeyValuePair<AssetType, int>(AssetType.UserInfo, userInfo.Id));
//     }
//
//     [TestCase("Heroes", "2023-05-27")]
//     [TestCase("Grant", "2023-06-27")]
//     public void ContestProviderTests(string theme, string dateTimeString)
//     {
//         var contest = new Contest
//         {
//             EndDate = DateTime.Parse(dateTimeString),
//             Theme = theme
//         };
//
//         //Insert
//         CachedDataStore.Insert(contest, AssetType.Contest);
//
//         //Get by id
//         Contest EnsureInfoById(Contest expected)
//         {
//             var actual = CachedDataStore.Get(expected.Id, AssetType.Contest) as Contest;
//             Assert.That(actual.Id, Is.EqualTo(expected.Id));
//             Assert.That(actual.Theme, Is.EqualTo(expected.Theme));
//             Assert.That(actual.EndDate, Is.EqualTo(expected.EndDate));
//             return actual;
//         }
//
//         EnsureInfoById(contest);
//
//         //Update
//         contest.Theme = $"updated_{theme}";
//         contest.EndDate = DateTime.Parse(dateTimeString).AddDays(5);
//         var updateParams = ContestParams.Theme | ContestParams.EndDate;
//         CachedDataStore.Update(contest, AssetType.Contest, (long)updateParams);
//         EnsureInfoById(contest);
//
//         IdentityMap.Add(new KeyValuePair<AssetType, int>(AssetType.Contest, contest.Id));
//     }
//
//     [TestCase("https://learn.microsoft.com/en-us/s-studio-ssms?redirectedfrom=MSDN&view=sql-server-ver16")]
//     [TestCase("https://server-management-studio-ssms?redirectedfrom=MSDN&view=sql-server-ver16")]
//     public void FileInfoProviderTests(string path)
//     {
//         var fileInfo = new FileInfo
//         {
//             Path = path
//         };
//
//         //Insert
//         CachedDataStore.Insert(fileInfo, AssetType.FileInfo);
//
//         //Get by id
//         FileInfo EnsureInfoById(FileInfo expected)
//         {
//             var actual = CachedDataStore.Get(expected.Id, AssetType.FileInfo) as FileInfo;
//             Assert.That(actual.Id, Is.EqualTo(expected.Id));
//             Assert.That(actual.Path, Is.EqualTo(expected.Path));
//             return actual;
//         }
//
//         EnsureInfoById(fileInfo);
//
//         //Update
//         fileInfo.Path = $"updated_{path}";
//         var updateParams = FileInfoParams.Path;
//         CachedDataStore.Update(fileInfo, AssetType.FileInfo, (long)updateParams);
//         EnsureInfoById(fileInfo);
//
//         IdentityMap.Add(new KeyValuePair<AssetType, int>(AssetType.FileInfo, fileInfo.Id));
//     }
//
//     private void InsertUserInfo(string name, string email)
//     {
//         var userInfo = new UserInfo
//         {
//             Name = name,
//             Email = email,
//             RegistrationDate = DateTime.Today,
//             RefId = Guid.NewGuid().ToString()
//         };
//
//         //Insert
//         CachedDataStore.Insert(userInfo, AssetType.UserInfo);
//         IdentityMap.Add(new KeyValuePair<AssetType, int>(AssetType.UserInfo, userInfo.Id));
//     }
//
//     private void InsertContest(string theme, string dateTimeString)
//     {
//         var contest = new Contest
//         {
//             EndDate = DateTime.Parse(dateTimeString),
//             Theme = theme
//         };
//
//         //Insert
//         CachedDataStore.Insert(contest, AssetType.Contest);
//         IdentityMap.Add(new KeyValuePair<AssetType, int>(AssetType.Contest, contest.Id));
//     }
//
//     private void InsertFileInfo(string path)
//     {
//         var fileInfo = new FileInfo
//         {
//             Path = path
//         };
//
//         //Insert
//         CachedDataStore.Insert(fileInfo, AssetType.FileInfo);
//         IdentityMap.Add(new KeyValuePair<AssetType, int>(AssetType.FileInfo, fileInfo.Id));
//     }
//
//     [Test]
//     public void SubmissionProviderTests()
//     {
//         InsertUserInfo("Jimbru", "jimbru.rocks@hotmail.com");
//         InsertContest("Circus Time 1 2 3", "2019-01-01");
//         InsertFileInfo("https://github.com/actions/setup-dotnet");
//         var submissionInfo = new Submission
//         {
//             UserId = IdentityMap.Where(kv => kv.Key == AssetType.UserInfo).Select(kv => kv.Value).First(),
//             Caption = "The best photo ever",
//             UploadedOn = DateTime.Parse("2019-02-01"),
//             FileInfoId = IdentityMap.Where(kv => kv.Key == AssetType.FileInfo).Select(kv => kv.Value).First(),
//             ContestId = IdentityMap.Where(kv => kv.Key == AssetType.Contest).Select(kv => kv.Value).First(),
//             RefId = Guid.NewGuid().ToString()
//         };
//
//         //Insert
//         CachedDataStore.Insert(submissionInfo, AssetType.Submission);
//         IdentityMap.Add(new KeyValuePair<AssetType, int>(AssetType.Submission, submissionInfo.Id));
//
//         //Get by id
//         Submission EnsureInfoById(Submission expected)
//         {
//             var actual = CachedDataStore.Get(expected.Id, AssetType.Submission) as Submission;
//             Assert.That(actual.Id, Is.EqualTo(expected.Id));
//             Assert.That(actual.Caption, Is.EqualTo(expected.Caption));
//             Assert.That(actual.UploadedOn, Is.EqualTo(expected.UploadedOn));
//             Assert.That(actual.FileInfoId, Is.EqualTo(expected.FileInfoId));
//             Assert.That(actual.ContestId, Is.EqualTo(expected.ContestId));
//             Assert.That(actual.RefId, Is.EqualTo(expected.RefId));
//             return actual;
//         }
//
//         EnsureInfoById(submissionInfo);
//
//         //Update
//         submissionInfo.Caption = $"updated_{submissionInfo.Caption}";
//         submissionInfo.UploadedOn = submissionInfo.UploadedOn.AddDays(5);
//         var updateParams = SubmissionParams.Caption | SubmissionParams.UploadedOn;
//         CachedDataStore.Update(submissionInfo,AssetType.Submission, (long)updateParams);
//         EnsureInfoById(submissionInfo);
//     }
//
//     [OneTimeTearDown]
//     public void TearDown()
//     {
//         foreach (var id in IdentityMap.Where(kv => kv.Key == AssetType.VoteInfo).Select(kv => kv.Value))
//             Assert.IsTrue(CachedDataStore.Delete(id, AssetType.VoteInfo));
//
//         foreach (var id in IdentityMap.Where(kv => kv.Key == AssetType.ScoreInfo).Select(kv => kv.Value))
//             Assert.IsTrue(CachedDataStore.Delete(id, AssetType.ScoreInfo));
//
//         foreach (var id in IdentityMap.Where(kv => kv.Key == AssetType.Submission).Select(kv => kv.Value))
//             Assert.IsTrue(CachedDataStore.Delete(id, AssetType.Submission));
//
//         foreach (var id in IdentityMap.Where(kv => kv.Key == AssetType.FileInfo).Select(kv => kv.Value))
//             Assert.IsTrue(CachedDataStore.Delete(id, AssetType.FileInfo));
//
//         foreach (var id in IdentityMap.Where(kv => kv.Key == AssetType.UserInfo).Select(kv => kv.Value))
//             Assert.IsTrue(CachedDataStore.Delete(id, AssetType.UserInfo));
//
//         foreach (var id in IdentityMap.Where(kv => kv.Key == AssetType.Contest).Select(kv => kv.Value))
//             Assert.IsTrue(CachedDataStore.Delete(id, AssetType.Contest));
//         
//         using SqlConnection connection = new(ConnectionString);
//         connection.Open();
//         using var command = connection.CreateCommand();
//         command.CommandType = CommandType.StoredProcedure;
//         command.CommandText = "[dbo].[Cleanup]";
//         command.ExecuteNonQuery();
//     }
// }