window.reservaSession = {

    start: function (dotNetHelper) {

        function activity() {
            dotNetHelper.invokeMethodAsync("UserActivity");
        }

        document.addEventListener("click", activity);
        document.addEventListener("mousemove", activity);
        document.addEventListener("keydown", activity);
        document.addEventListener("scroll", activity);
        document.addEventListener("touchstart", activity);
    },

    stop: function () {
    }
};