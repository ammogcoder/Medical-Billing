// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project. 
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc. 
//
// To add a suppression to this file, right-click the message in the 
// Error List, point to "Suppress Message(s)", and click 
// "In Project Suppression File". 
// You do not need to add suppressions to this file manually. 


[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Portability", "CA1903:UseOnlyApiFromTargetedFramework", MessageId = "System.Data.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Scope = "type", Target = "BookLibrary.Applications.Controllers.EntityController")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member", Target = "BookLibrary.Applications.Controllers.EntityController.#.ctor(BookLibrary.Applications.Services.EntityService,System.Waf.Applications.Services.IMessageService,BookLibrary.Applications.ViewModels.ShellViewModel)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "BookList", Scope = "type", Target = "BookLibrary.Applications.ViewModels.BookListViewModel")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "BookList", Scope = "member", Target = "BookLibrary.Applications.ViewModels.ShellViewModel.#BookListView")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "BookList", Scope = "type", Target = "BookLibrary.Applications.Views.IBookListView")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Portability", "CA1903:UseOnlyApiFromTargetedFramework", MessageId = "System.ComponentModel.DataAnnotations, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "CanSave", Scope = "member", Target = "BookLibrary.Applications.Controllers.EntityController.#Save()")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "BookLibrary.Applications.ViewModels.ShellViewModel.#.ctor(BookLibrary.Applications.Views.IShellView,System.Waf.Applications.Services.IMessageService)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "BookLibrary.Applications.ViewModels.BookListViewModel.#Filter(BookLibrary.Domain.Book)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "BookLibrary.Applications.ViewModels.PersonListViewModel.#Filter(BookLibrary.Domain.Person)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "BookLibrary.Applications.ViewModels.BookListViewModel.#Filter(BookLibrary.Applications.DataModels.BookDataModel)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "BookLibrary.Applications.Controllers.ApplicationController.#Shutdown()")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "BookLibrary.Applications.ViewModels.ShellViewModel.#.ctor(BookLibrary.Applications.Views.IShellView,BookLibrary.Applications.Services.IPresentationService,System.Waf.Applications.Services.IMessageService)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1", Scope = "member", Target = "BookLibrary.Applications.ViewModels.ShellViewModel.#.ctor(BookLibrary.Applications.Views.IShellView,BookLibrary.Applications.Services.IPresentationService,System.Waf.Applications.Services.IMessageService)")]
