// $(document).ready(function () {
//   // Handle click event on menu items with submenus
//   $(".menu li:has(.sub-menu) > a").on("click", function (e) {
//     e.preventDefault(); // Prevent default link behavior

//     const submenu = $(this).siblings(".sub-menu");
//     const isVisible = submenu.is(":visible");

//     // Close all open submenus
//     $(".sub-menu").removeClass("show-element");

//     // Open the clicked submenu if it was not already open
//     if (!isVisible) {
//       $(".sub-menu").addClass("show-element");
//     }
//   });

//   // Close submenu if clicking outside the menu
//   $(document).on("click", function (e) {
//     if (!$(e.target).closest(".menu").length) {
//       $(".sub-menu").removeClass("show-element");
//     }
//   });
// });
