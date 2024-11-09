using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace devarts.Models
{
    [MetadataType(typeof(PostMetaData))]
    [Table("Posts")]
    public class Post
    {
        /// id
        [Key]
        public int Id { get; set; }

        // id zdjęcia głównego
        public int ImageId { get; set; }

        // nick autora postu        
        public string MainPicture { get; set; }

        // nick autora postu        
        public string AuthorName { get; set; }

        /// nazwa opisywanego postu
        public string PostName { get; set; }

        // nazwa opisywanego postu en
        public string PostNameEng { get; set; }

        // kategoria autora postu
        public string CategoryName { get; set; }

        /// typ postu - aktualności/blog
        public string Type { get; set; }

        // treść postu
        public string PostContent { get; set; }

        // wstęp po polsku
        public string PostContentShort { get; set; }

        // treść postu en
        public string PostContentEng { get; set; }

        // wstęp po angielsku
        public string PostContentShortEng { get; set; }

        /// link do postu
        public string PostLink { get; set; }

        // tagi
        public string Tags { get; set; }

        /// data dodania
        public string PostDate { get; set; }

        /// data dodania w formacie DateTime
        public DateTime PostDateOkay { get; set; }

        // data modyfikacji
        public string ModifiedDate { get; set; }

        /// ocena komentarza
        public int PostRate { get; set; }

        /// liczba wyświetleń
        public int PostShow { get; set; }

        // liczba komentarzy
        public int PostCommentsCount { get; set; }

        // liczba polubień
        public int PostLikes { get; set; }

        /// możliwość komentowania
        public bool AllowComments { get; set; }

        /// opublikowany
        public bool IsPublished { get; set; }

        /// NOWO DODANE
        public string BlogCathegory { get; set; }
        public string PortfolioCathegory { get; set; }        
        public string InterviewCathegory { get; set; }
    }

    public class PostMetaData
    {
        public int Id { get; set; }

        [Display(Name = "Autor")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagany.")]
        public string AuthorName { get; set; }

        [Display(Name = "Tytuł")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagany.")]
        public string PostName { get; set; }

        [Display(Name = "Tytuł po angielsku")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string PostNameEng { get; set; }

        [Display(Name = "Kategoria")]
        //[Required(ErrorMessage = "{0} jest wymagana.")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string CategoryName { get; set; }

        [Display(Name = "Typ wpisu")]
        [Required(ErrorMessage = "{0} jest wymagany.")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Type { get; set; }

        [AllowHtml]
        [Display(Name = "Treść postu")]
        [DataType(DataType.MultilineText)]
        [Column("PostContent", TypeName = "ntext")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagana.")]
        public string PostContent { get; set; }

        [AllowHtml]
        [Display(Name = "Wstęp postu")]
        [DataType(DataType.MultilineText)]
        [Column("PostContentShort", TypeName = "ntext")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "{0} jest wymagany.")]
        public string PostContentShort { get; set; }

        [AllowHtml]
        [Display(Name = "Treść postu EN")]
        [DataType(DataType.MultilineText)]
        [Column("PostContentEng", TypeName = "ntext")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string PostContentEng { get; set; }

        [AllowHtml]
        [Display(Name = "Wstęp postu EN")]
        [DataType(DataType.MultilineText)]
        [Column("PostContentShortEng", TypeName = "ntext")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string PostContentShortEng { get; set; }

        [Required(ErrorMessage = "{0} jest wymagana.")]
        [StringLength(40, ErrorMessage = "Nazwa folderu rasy nie może być dłuższa niż 40 znaków!")]
        [Display(Name = "Nazwa folderu posta (bez spacji, poskich znaków, z dozwolonym podkreślnikiem oraz myślnikiem)")]
        [RegularExpression(@"^[a-zA-Z0-9-_]+$", ErrorMessage = "Dozwolone litery, cyfry, podkreślnik i myślnik bez spacji i polskich znaków - do 40 znaków!")]
        public string PostLink { get; set; }

        [Display(Name = "Tagi")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Tags { get; set; }

        [Display(Name = "Data utworzenia")]
        public string PostDate { get; set; }

        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        [Display(Name = "Data utworzenia do sortowania")]
        public DateTime PostDateOkay { get; set; }

        [Display(Name = "Data modyfikacji")]
        public string ModifiedDate { get; set; }

        [Display(Name = "Ocena")]
        public int PostRate { get; set; }

        [Display(Name = "Wyświetleń")]
        public int PostShow { get; set; }

        // liczba komentarzy
        public int PostCommentsCount { get; set; }

        // liczba polubień
        public int PostLikes { get; set; }

        // możliwość komentowania
        public bool AllowComments { get; set; }

        [Display(Name = "Opublikowany")]
        public bool IsPublished { get; set; }        
    }

    public class PostCathegory
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }

    public class PostViewModel
    {
        public string MainPicture { get; set; }

        public string AuthorName { get; set; }

        public string PostName { get; set; }

        public string PostNameEng { get; set; }

        public string PostShortEng { get; set; }

        public string CategoryName { get; set; }

        public string PostContentShort { get; set; }

        public string PostContent { get; set; }

        public string PostContentShortEng { get; set; }

        public string PostContentEng { get; set; }

        public string Tags { get; set; }

        public string PostDate { get; set; }

        public int PostRate { get; set; }

        public int PostShow { get; set; }

    }

    public class PostFilterViewModel
    {
        public string AuthorName { get; set; }

        public string PostName { get; set; }

        public string PostNameEng { get; set; }

        public string CategoryName { get; set; }

        public string PostContentShort { get; set; }

        public string PostContent { get; set; }

        public string PostContentShortEng { get; set; }

        public string PostContentEng { get; set; }

        public string Tags { get; set; }

        public string PostDate { get; set; }

        public int PostRate { get; set; }

        public int PostShow { get; set; }

        public string HostIP { get; set; }
    }

    public class DataTablePostViewModel
    {
        public int Id { get; set; }
        public string PostName { get; set; }
        public string CategoryName { get; set; }
        public string PostDate { get; set; }
        public int PostShow { get; set; }
        public string PostLink { get; set; }
        public string mainImage { get; set; }
        /// ... dodawane w miarę potrzeby
    }

    [Table("PostImages")]
    public class PostImage
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string PostLink { get; set; }
        public string ImageFileName { get; set; }
        public string ImageFileSize { get; set; }
        public string ImageDate { get; set; }
        public bool IsPublished { get; set; }

        public int ImageIndex { get; set; }
        public string ImageAlt { get; set; }
    }

    public class PostAndImageViewModel
    {
        public IQueryable<Post> entries { get; set; }
        public PostImage mainImg { get; set; }
    }

    public class PostWithMainImageUrl
    {
        // id        
        public int Id { get; set; }

        // id zdjęcia głównego
        public int ImageId { get; set; }

        public string AuthorName { get; set; }

        public string PostName { get; set; }

        public string PostNameEng { get; set; }

        public string CategoryName { get; set; }

        public string Type { get; set; }

        public string PostContentShort { get; set; }

        public string PostContent { get; set; }

        public string PostContentShortEng { get; set; }

        public string PostContentEng { get; set; }

        public string PostLink { get; set; }

        public string Tags { get; set; }

        public string PostDate { get; set; }

        public DateTime PostDateOkay { get; set; }

        public int PostRate { get; set; }

        public int PostShow { get; set; }

        public bool IsPublished { get; set; }

        /// po złączeniu dodatkowo link do zdjęcia głównego
        public string mainImage { get; set; }

        /// lista zdjęć przypisanych do posta
        public List<PostImage> imagesByPost { get; set; }
    }

    // model widoku ładowany podczas edycji postu
    public class PostWithAllImages
    {
        public Post Post { get; set; }
        public List<PostImage> imgFiles { get; set; }
    }

    [Table("Comments")]
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public int PostId { get; set; }
        public int ParentCommentId { get; set; }

        public string PostLink { get; set; }
        public string Nick { get; set; }
        public string Email { get; set; }
        public string CommentDate { get; set; }

        public bool Notify { get; set; }
        public bool IsPublished { get; set; }
    }
}