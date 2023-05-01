// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var ulNavigationNode = document.querySelector('.navbar-nav')
if (ulNavigationNode) {
    console.log(ulNavigationNode)
    var liNodes = ulNavigationNode.getElementsByTagName('li')
    if (liNodes) {
        for (var i = 0; i < liNodes.length; i++) {
            console.log(liNodes[i])
        }
    }

    ulNavigationNode.onclick = function (event) {
           console.log(event.target)
    }

}
