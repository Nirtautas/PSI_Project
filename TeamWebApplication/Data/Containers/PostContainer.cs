using System.Globalization;
using TeamWebApplication.Models;
using TeamWebApplication.ExtensionMethods;

namespace TeamWebApplication.Data
{
    public class PostContainer : IPostContainer
    {
        public string FetchingPath { get; }
        private int postIdCounter;
        public ICollection<Post> PostList { get; }

        public PostContainer(string fetchingPath = "./TextData/PostData.txt")
        {
            this.FetchingPath = fetchingPath;
            PostList = new List<Post>();
            FetchPosts();
        }

        public void FetchPosts()
        {
            string? readString;
            string[]? splitString;
            using (StreamReader? reader = new StreamReader(FetchingPath))
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
                                id: Int32.Parse(splitString[0]),                                                         
                                courseId: Int32.Parse(splitString[1]),                                                             
                                name: splitString[2],                                                                        
                                //creationDate: DateTime.ParseExact(splitString[3], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                                creationDate: DateTime.ParseExact(splitString[3], "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture), 
                                IsVisible: Boolean.Parse(splitString[4]),                                                         
                                PostType: (PostType)Enum.Parse(typeof(PostType), splitString[5]),                                   
                                textContent: splitString[6]                                                                          
                            );
                            PostList.Add(textPost);
                            break;
                        case "Link":
                            LinkPost linkPost = new LinkPost(
                                id: Int32.Parse(splitString[0]),                                                           
                                courseId: Int32.Parse(splitString[1]),                                                             
                                name: splitString[2],
                                //creationDate: DateTime.ParseExact(splitString[3], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                                creationDate: DateTime.ParseExact(splitString[3], "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture),
                                IsVisible: Boolean.Parse(splitString[4]),                                                       
                                PostType: (PostType)Enum.Parse(typeof(PostType), splitString[5]),                               
                                linkContent: splitString[6]                                                                      
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
            using (StreamWriter? writer = new StreamWriter(FetchingPath))
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

        public void DeletePost(Post post)
        {
            PostList.Remove(post);
            WritePosts();
        }
    }
}
