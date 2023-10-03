using System.Globalization;
using TeamWebApplication.Models;

namespace TeamWebApplication.Data
{
    public class PostContainer : IPostContainer
    {
        private int postIdCounter;
        public ICollection<Post> PostList { get; }

        public PostContainer()
        {
            PostList = new List<Post>();
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
                                Int32.Parse(splitString[1]),                                                              //postId
                                splitString[2],                                                                           //Name
                                DateTime.ParseExact(splitString[3], "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture), //creationDate
                                Boolean.Parse(splitString[4]),                                                            //Is Visible?
                                (PostType)Enum.Parse(typeof(PostType), splitString[5]),                                   //Type
                                splitString[6]                                                                            //Text Data
                            );
                            PostList.Add(textPost);
                            break;
                        case "Link":
                            LinkPost linkPost = new LinkPost(
                                Int32.Parse(splitString[0]),                                                              //PostId
                                Int32.Parse(splitString[1]),                                                              //postId
                                splitString[2],                                                                           //Name
                                DateTime.ParseExact(splitString[3], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), //creationDate
                                Boolean.Parse(splitString[4]),                                                            //Is Visible?
                                (PostType)Enum.Parse(typeof(PostType), splitString[5]),                                   //Type
                                splitString[6]                                                                            //Link Data
                            );
                            PostList.Add(linkPost);
                            break;
                    }
                }
            }
        }

        public Post? GetPost(int postId)
        {
            Post? post = PostList.SingleOrDefault(post => post.PostId == postId);
            return post;
        }

        public void PrintPostList()
        {
            foreach (Post post in PostList)
                System.Diagnostics.Debug.WriteLine(post.ToString());
        }

        public void WritePosts()
        {
            using (StreamWriter? writer = new StreamWriter("./TextData/PostData.txt"))
            {
                writer.WriteLine(postIdCounter);
                foreach (Post post in PostList)
                    writer.WriteLine(post.ToString());
            }
        }

        public int CreatePost(Post post, int currentCourseId)
        {
            post.CourseId = currentCourseId;
            post.PostId = postIdCounter;
            postIdCounter++;
            post.CreationDate = DateTime.Now;
            PostList.Add(post);
            return post.PostId;
        }
        public int CreatePost(int currentCourseId, String name = "Bendra", String textContent = "Welcome to the course!")
        {
            TextPost post = new TextPost();
            post.Name = name;
            post.PostType = PostType.Text;
            post.TextContent = textContent;
            post.CourseId = currentCourseId;
            post.PostId = postIdCounter;
            postIdCounter++;
            post.CreationDate = DateTime.Now;
            PostList.Add(post);
            return post.PostId;
        }

        public void DeletePost(Post post)
        {
            PostList.Remove(post);
            WritePosts();
        }
    }
}
