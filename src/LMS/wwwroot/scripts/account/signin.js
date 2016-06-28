(function (XHR, $) {
    "use strict";

    window.addEventListener("DOMContentLoaded", init);

    var signInForm = {}; 
    var signInButton = {};
    var submitButton = {};
    var userName = {};
    var password = {};

    function init() {
        signInForm = $.getContainer(".signInForm");

        signInButton = $.getContainer("#signInUser");
        signInButton.addEventListener("click", showForm);

        submitButton = $.getContainer("#submit");

        if (submitButton !== null) {
            submitButton.addEventListener("click", signIn);
        }

        userName = $.getContainer("#userName");
        password = $.getContainer("#password");

        document.addEventListener("keydown", canSubmitForm);
        window.addEventListener("click", hideForm);
    };

    function signIn(e) {
        e.preventDefault();

        if (isValid()) {

            XHR.post({
                url: "/account/signin",
                data: JSON.stringify({ userName: userName.value, password: password.value }),
                success: successHandler,
                error: errorHandler
            });
        }
    };

    function successHandler(data) {
        var signInButton = $.getContainer("#signInUser");
        signInButton.textContent = JSON.parse(data).userName;
        signInButton.setAttribute("href", "/profile");

        if (signInButton.hasAttribute("data-no-route")) {
            signInButton.removeAttribute("data-no-route");
        }

        var signUpButton = $.getContainer("#signUpOut");
        signUpButton.textContent = "Sign Out";
        signUpButton.setAttribute("href", "/signout");

        $.fadeOut(signInForm);

        signInForm.addEventListener("animationend", function () {
            signInForm.classList.add("hidden");
            signInForm.classList.remove("fade-out");
        });
    };

    function errorHandler(data) {
        signInForm.classList.add("shake");
        signInForm.addEventListener("animationend", function shake(e) {
            signInForm.classList.remove("shake");
            signInForm.removeEventListener("animationend", shake);
        });
    };

    function showForm(e) {
        if (e.target.textContent === "Sign In") {
            signInForm.classList.remove("hidden")
            $.fadeIn(signInForm);
            signInForm.addEventListener("animationend", function animationDone(e) {
                if (e.animationName === "fade-in" || e.animationName === "fadeInDown") {
                    signInForm.classList.remove("fade-in");
                    signInForm.removeEventListener("animationend", animationDone);
                    userName.focus();
                }
            });
        }
    };

    function hideForm(e) {
        var target = e.target,
            formIsVisible = !$.hasClass(signInForm, "hidden");

        if (formIsVisible && target !== signInButton && target !== signInForm && !signInForm.contains(target)) {
            $.fadeOut(signInForm);

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

        errorHandler();
        return false;
    };

    function canSubmitForm(e) {
        if (e.key.toLowerCase() === "enter") {

            // fake button press
            submitButton.classList.toggle("active");

            // fake button release
            setTimeout(function() {
                submitButton.classList.toggle("active");
            }, 120);

            // submit
            if (isValid()) {
                submitButton.click();
            }
        }
    };

})(XHR, baseUtils);