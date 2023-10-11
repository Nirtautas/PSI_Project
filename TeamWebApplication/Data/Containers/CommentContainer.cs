using System.Globalization;
using TeamWebApplication.ExtensionMethods;
using TeamWebApplication.Models;

namespace TeamWebApplication.Data
{
    public sealed class CommentContainer : ICommentContainer
    {
        public string FetchingPath { get; }
        private int commentIdCounter;
        public ICollection<Comment> CommentList { get; }
        public CommentContainer(string fetchingPath = "./TextData/CommentData.txt")
        {
            this.FetchingPath = fetchingPath;
            CommentList = new List<Comment>();
            FetchComments();
        }

        public void FetchComments()
        {
            string? readString;
            string[]? splitString;
            using (StreamReader? reader = new StreamReader(FetchingPath))
            {
                if ((readString = reader.ReadLine()) != null)
                    commentIdCounter = Int32.Parse(readString);

                while ((readString = reader.ReadLine()) != null)
                {
                    splitString = readString.Split(';');
                    Comment comment = new Comment(
                        commentId: Int32.Parse(splitString[0]),                                                            
                        courseId: Int32.Parse(splitString[1]),     
                        userId: Int32.Parse(splitString[2]),                                     
                        usersNameThatCommented: splitString[3],                
                        usersSurnameThatCommented: splitString[4],               
                        commentCreationTime: DateTime.ParseExact(splitString[5], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),                                                                      //description
                        userComment: splitString[6]                                                        
                    );
                    CommentList.Add(comment);
                }
            }
        }
        public void CreateComment(Comment comment, int currentCourseId, int loggedInUserId, IUserContainer _userContainer)
        {
            comment.CommentId = commentIdCounter;
            commentIdCounter++;
            comment.CourseId = currentCourseId;
            var user = _userContainer.userList.SingleOrDefault(user => user.UserId == loggedInUserId);
            comment.UserId = user.UserId;
            comment.UsersNameThatCommented = user.Name;
            comment.UsersSurnameThatCommented = user.Surname;
            comment.CommentCreationTime = DateTime.Now;
            CommentList.Add(comment);
            WriteComments();
        }

        public void DeleteComment(Comment comment)
        {
            CommentList.Remove(comment);
            WriteComments();
        }

        public void PrintComments()
        {
            foreach (Comment comment in CommentList)
                System.Diagnostics.Debug.WriteLine(comment.FormattedToString());
        }
        public void WriteComments()
        {
            using (StreamWriter? writer = new StreamWriter(FetchingPath))
            {
                writer.WriteLine(commentIdCounter);
                foreach (Comment comment in CommentList)
                    writer.WriteLine(comment.FormattedToString());
            }
        }
    }
}