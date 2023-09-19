using System.Globalization;
using TeamWebApplication.Models;

namespace TeamWebApplication.Data
{
    public interface IPostContainer
    {
        void FetchPosts();
        void PrintPostList();
        void WritePosts();
        ICollection<Post> postList { get; }
    }
    public class PostContainer : IPostContainer
    {
        private int postIdCounter;
        public ICollection<Post> postList { get; }

        public PostContainer()
        {
            postList = new List<Post>();
            FetchPosts();
        }

        public void FetchPosts()
        {
            string? readString;
            string[]? splitString;
            using (StreamReader? reader = new StreamReader("./TextData/PostData.txt"))
            {
                if ((readString = reader.ReadLine()) != null)
                    postIdCounter = Int32.Parse(readString);

                while ((readString = reader.ReadLine()) != null)
                {
                    splitString = readString.Split(';');
                    switch (splitString[5])
                    {
                        case "Text":
                            TextPost textPost = new TextPost(
                                Int32.Parse(splitString[0]),                                                              //PostId
                                Int32.Parse(splitString[1]),                                                              //CourseId
                                splitString[2],                                                                           //Name
                                DateTime.ParseExact(splitString[3], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), //creationDate
                                Boolean.Parse(splitString[4]),                                                            //Is Visible?
                                (PostType)Enum.Parse(typeof(PostType), splitString[5]),                                   //Type
                                splitString[6]                                                                            //Text Data
                            );
                            postList.Add(textPost);
                            break;
                    }
                }
            }
        }

        public void PrintPostList()
        {
            foreach (Post post in postList)
                System.Diagnostics.Debug.WriteLine(post.ToString());
        }

        public void WritePosts()
        {
            using (StreamWriter? writer = new StreamWriter("./TextData/PostData.txt"))
            {
                writer.WriteLine(postIdCounter);
                foreach (Post post in postList)
                    writer.WriteLine(post.ToString());
            }
        }
    }
}
