$(document).ready(function () {
	BlinkDonateHeart();
});

function BlinkDonateHeart() {
	for (var i = 0; i < 10; i++) {
		$('#heart-icon')
			.fadeOut(350)
			.fadeIn(350);
	}
}

function IsMobile() {
	return (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent));
}

function SetHand(hand, handValue) {
	//if (confirm('Your choice: ' + hand + '. Are you sure?')) {
	$('#OpponentForm').submit();
	//}
}

function InitClipboard() {
	try {
		new ClipboardJS('#butCopy');
	}
	catch { }
}

function InitShareButton() {
	if (IsMobile()) {
		$('#butShare').removeClass("d-none");
		$('#butShare').click(function () {
			try {
				navigator.share({ title: document.title, url: $('#txtCopy').val() });
				console.log("Data was shared successfully");
			} catch (err) {
				console.error("Share failed:", err.message);
			}
		});
	}
}

function CheckGameResult(id) {
	setTimeout(() => { GetGameResultTimer(id); }, 5000);
}

function GetGameResultTimer(id) {
	var n = 0;
	var timePause = 5000;
	let timerId = setTimeout(function tick() {

		$.ajax({
			url: "/api/check-result/" + id,
			method: 'GET',
			dataType: 'json',
			contentType: 'application/json; charset=utf-8',
			success: function (data) {
				if (data.result > 0) {
					clearTimeout(timerId);
					location.reload();
				}
				else {
					timePause += 250;
					if (n++ > 50) {
						clearTimeout(timerId);
					}
				}
			},
			fail: function (jqXHR, textStatus) {
				clearTimeout(timerId);
			}
		})

		timerId = setTimeout(tick, timePause);
	}, timePause);
}

function GetGameResult(id) {
	
}
