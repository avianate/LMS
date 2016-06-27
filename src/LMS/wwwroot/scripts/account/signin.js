(function (XHR) {
    "use strict";

    window.addEventListener("DOMContentLoaded", init);

    var signInForm = {}; 
    var signInButton = {};
    var submitButton = {};
    var userName = {};
    var password = {};

    function init() {
        signInForm = document.querySelector(".signInForm");

        signInButton = document.querySelector("#signInUser");
        signInButton.addEventListener("click", showForm);

        submitButton = document.querySelector("#submit");
        submitButton.addEventListener("click", signIn);

        userName = document.querySelector("#userName");
        password = document.querySelector("#password");

        document.addEventListener("keyup", canSubmitForm);
        window.addEventListener("click", hideForm);
    };

    function signIn(e) {
        e.preventDefault();

        if (isValid) {

            XHR.post({
                url: "/account/signin",
                data: JSON.stringify({userName: userName.value, password: password.value}),
                success: function (data) {
                    var signInButton = document.querySelector("#signInUser");
                    signInButton.textContent = JSON.parse(data).userName;
                    signInButton.setAttribute("href", "/profile");

                    if (signInButton.hasAttribute("data-no-route")) {
                        signInButton.removeAttribute("data-no-route");
                    }

                    var signUpButton = document.querySelector("#signUpOut");
                    signUpButton.textContent = "Sign Out";
                    signUpButton.setAttribute("href", "/signout");

                    signInForm.classList.remove("fade-in");
                    signInForm.classList.add("fade-out");

                    signInForm.addEventListener("animationend", function () {
                        signInForm.classList.add("hidden");
                        signInForm.classList.remove("fade-out");
                    });
                }
            });
        }
    };

    function hideForm(e) {
        var target = e.target,
            formIsVisible = !signInForm.classList.contains("hidden");

        if (formIsVisible && target !== signInButton && target !== signInForm && !signInForm.contains(target)) {
            signInForm.classList.remove("fade-in");
            signInForm.classList.add("fade-out");
            signInForm.addEventListener("animationend", function hide() {
                signInForm.classList.add("hidden");
                signInForm.removeEventListener("animationend", hide);
            });
        }
    };

    function isValid() {
        if (userName.value.length >= 8 && password.value.length >= 8)
        {
            return true;
        }

        return false;
    };

    function canSubmitForm(e) {
        if (e.key.toLowerCase() === "enter") {

            if (isValid()) {
                submitButton.click();
            }
        }
    };

    function showForm(e) {
        if (e.target.textContent === "Sign In") {
            signInForm.classList.remove("hidden")
            signInForm.classList.remove("fade-out");
            signInForm.classList.add("fade-in");
        }
    };

})(XHR);