namespace DotNetAPI.DTOs
{
    public partial class EditPostDTO
    {
        public int PostId {get; set;}
        public string PostTitle {get; set;} = "";
        public string PostContent {get; set;} = "";
    }
}