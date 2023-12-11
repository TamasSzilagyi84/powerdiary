namespace Dtos.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum ChatMessageType
    {
        [Display(Name = "Enters")]
        Enter = 0,

        [Display(Name = "Leaves")]
        Leave = 1,

        [Display(Name = "High fives")]
        HighFive = 2,

        [Display(Name = "Comment")]
        Comment = 3
    }
}
