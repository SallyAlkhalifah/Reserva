namespace Reserva.wwwroot.js
{
    public class header
    {
        headerScrollStarted = false;

        window.initHeaderScroll = function () {

            if (headerScrollStarted)
                return;

            headerScrollStarted = true;

            function updateHeader() {

                const header =
                    document.querySelector(".main-header");

                if (!header)
                    return;

                if (window.scrollY > 40) {

                    header.classList.add("header-scrolled");

                } else {

                    header.classList.remove("header-scrolled");

                }
            }


            window.addEventListener(
                "scroll",
                updateHeader,
                { passive: true }
            );


            updateHeader();
        };
    }
}
