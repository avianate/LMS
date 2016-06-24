(function (XHR) {
    "use strict";
    
    var button = document.querySelector("#submit");
    button.addEventListener("click", signIn);

    var userName = document.querySelector("#userName");
    var password = document.querySelector("#password");

    document.addEventListener("keyup", canSubmitForm);


    function signIn(e) {
        e.preventDefault();

        if (isValid) {

            XHR.post({
                url: "/account/signin",
                data: JSON.stringify({userName: userName.value, password: password.value}),
                success: function (data) {
                    // returns username. update the signin button's display and href
                    var signInButton = document.querySelector("#signInUser");
                    signInButton.textContent = JSON.parse(data);
                    signInButton.setAttribute("href", "/account/profile");

                    var signUpButton = document.querySelector("#signUpOut");
                    signUpButton.textContent = "Sign Out";
                    signUpButton.setAttribute("href", "/signout");

                    var signinForm = document.querySelector("signinForm");

                    signinForm.classList.remove("fade-in");
                    signinForm.classList.add("fade-out");

                    signinForm.addEventListener("animationend", function () {
                        signinForm.classList.add("hidden");
                        signinForm.classList.remove("fade-out");
                    });
                }
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
                button.click();
            }
        }
    }

})(XHR);