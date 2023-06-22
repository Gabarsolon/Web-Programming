namespace Ex3.Models
{
	public class PostDTO
	{
        public PostDTO(Post post, Topic topic)
        {
            Post = post;
            Topic = topic;
        }

        public Post Post { get; set; }
		public Topic Topic { get; set; }
	}

}
