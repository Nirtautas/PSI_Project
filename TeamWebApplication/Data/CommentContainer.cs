using System.Globalization;
using TeamWebApplication.Models;

namespace TeamWebApplication.Data
{
    public interface ICommentContainer
    {
        void FetchComments();
        void PrintComments();
        void WriteComments();
        ICollection<Comment> CommentList { get; }
    }

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
                        splitString[2],                  //user name
                        splitString[3],                  //user surname
                        DateTime.ParseExact(splitString[4], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), //creation date                                                                         //description
                        splitString[5]    //comment                                                        
                    );
                    CommentList.Add(comment);
                }
            }
        }
        public int CreateComment(Comment comment, int loggedInUserId, ICourseContainer _courseContainer, IUserContainer _userContainer)
        {
            comment.CommentId = commentIdCounter;
            commentIdCounter++;
            comment.CourseId = _courseContainer.currentCourseId;
            var user = _userContainer.userList.SingleOrDefault(user => user.UserId == loggedInUserId);
            comment.UsersNameThatCommented = user.Name;
            comment.UsersSurnameThatCommented = user.Surname;
            comment.CommentCreationTime = DateTime.Now;
            CommentList.Add(comment);
            WriteComments();
            return comment.CommentId;
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