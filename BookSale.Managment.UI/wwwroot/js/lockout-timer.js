function startLockoutCountdown(remainingSeconds, countdownSelector, alertSelector) {
    let remaining = remainingSeconds;
    const countdownEl = document.querySelector(countdownSelector);
    const alertEl = document.querySelector(alertSelector);

    function formatTime(seconds) {
        const m = Math.floor(seconds / 60);
        const s = seconds % 60;
        return `${m}m ${s}s`;
    }

    function updateCountdown() {
        if (remaining <= 0) {
            countdownEl.innerText = "You can try to login now.";
            setTimeout(() => {
                alertEl.style.display = "none";
            }, 1500);
            return;
        }

        countdownEl.innerText =
            `Account is locked out. Please try again in ${formatTime(remaining)}`;

        remaining--;
        setTimeout(updateCountdown, 1000);
    }

    updateCountdown();
}
