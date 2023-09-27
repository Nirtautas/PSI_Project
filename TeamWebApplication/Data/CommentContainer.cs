using System.Globalization;
using TeamWebApplication.Models;

namespace TeamWebApplication.Data
{
    public sealed class CommentContainer : ICommentContainer
    {
        private int commentIdCounter;
        public ICollection<Comment> CommentList { get; }
        public CommentContainer()
        {
            CommentList = new List<Comment>();
            FetchComments();
        }

        public void FetchComments()
        {
            string? readString;
            string[]? splitString;
            using (StreamReader? reader = new StreamReader("./TextData/CommentData.txt"))
            {
                if ((readString = reader.ReadLine()) != null)
                    commentIdCounter = Int32.Parse(readString);

                while ((readString = reader.ReadLine()) != null)
                {
                    splitString = readString.Split(';');
                    Comment comment = new Comment(
                        Int32.Parse(splitString[0]),     //commentId                                                         
                        Int32.Parse(splitString[1]),     //courseId
                        Int32.Parse(splitString[2]),     //userId                                     
                        splitString[3],                  //user name
                        splitString[4],                  //user surname
                        DateTime.ParseExact(splitString[5], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), //creation date                                                                         //description
                        splitString[6]    //comment                                                        
                    );
                    CommentList.Add(comment);
                }//repository class - kai duombazei
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
                System.Diagnostics.Debug.WriteLine(comment.ToString());
        }
        public void WriteComments()
        {
            using (StreamWriter? writer = new StreamWriter("./TextData/CommentData.txt"))
            {
                writer.WriteLine(commentIdCounter);
                foreach (Comment comment in CommentList)
                    writer.WriteLine(comment.ToString());
            }
        }
    }
}