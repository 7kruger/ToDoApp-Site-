using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebToDoApp.Models
{
    public class ToDoViewModel
    {
        [Required]
        [Display(Name = "Задача")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Описание задачи")]
        public string Description { get; set; }
    }

    public class EditViewModel
    {
        [HiddenInput]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Задача")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Описание задачи")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Выполнена")]
        public bool IsDone { get; set; }
    }
}