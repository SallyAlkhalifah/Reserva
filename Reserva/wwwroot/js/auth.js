namespace Reserva.wwwroot.js
{
    public class auth
    {
        window.reservaAuth = {

            saveUser: function (user) {
                localStorage.setItem(
                    "reserva_current_user",
                    JSON.stringify(user)
                );
            },

            getUser: function () {
                return localStorage.getItem(
                    "reserva_current_user"
                );
            },

            removeUser: function () {
                localStorage.removeItem(
                    "reserva_current_user"
                );
            }
        };

        // =========================================================
        // RESERVA HEADER SCROLL ANIMATION
        // =========================================================

        window.initHeaderScroll = function () {

            const header = document.querySelector(".main-header");

            if (!header) {
                return;
            }

            function handleScroll() {

                if (window.scrollY > 40) {
                    header.classList.add("header-scrolled");
                } else {
                    header.classList.remove("header-scrolled");
                }
            }

            // Initial state
            handleScroll();

            // Avoid registering multiple listeners
            if (!window.reservaHeaderScrollInitialized) {

                window.addEventListener("scroll", handleScroll, {
                    passive: true
                });

                window.reservaHeaderScrollInitialized = true;
            }
        };
    }
}
