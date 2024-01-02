<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="webcam_new.aspx.cs" Inherits="SMS.webcam_new" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="jQuery-webcam-master/jQuery-webcam-master/jquery.webcam.min.js"></script>
    
    <script src="jQuery-webcam-master/jQuery-webcam-master/jquery.webcam.js"></script>
    <title></title>
    
</head>
<body>
    <form id="form1" runat="server">
        <div id="webcam"></div>
    </form>
    <script type="text/javascript">
        $(document).ready(function () {
        $("#camera").webcam({
	width: 320,
	height: 240,
	mode: "callback",
	swffile: "/download/jscam_canvas_only.swf",
	onTick: function() {},
	onSave: function() {},
	onCapture: function() {},
	debug: function() {},
	onLoad: function() {}
            });
            onLoad: function() {

    var cams = webcam.getCameraList();
    for(var i in cams) {
        jQuery("#cams").append("<li>" + cams[i] + "</li>");
    }
            }
            debug: function (type, string) {
	$("#status").html(type + ": " + string);
            }
            onTick: function(remain) {

    if (0 == remain) {
        jQuery("#status").text("Cheese!");
    } else {
        jQuery("#status").text(remain + " seconds remaining...");
    }
            }
            onCapture: function () {

	jQuery("#flash").css("display", "block");
	jQuery("#flash").fadeOut("fast", function () {
		jQuery("#flash").css("opacity", 1);
	});

	webcam.save();
            }
            onSave: function(data) {

    var col = data.split(";");
    var img = image;

    for(var i = 0; i < 320; i++) {
        var tmp = parseInt(col[i]);
        img.data[pos + 0] = (tmp >> 16) & 0xff;
        img.data[pos + 1] = (tmp >> 8) & 0xff;
        img.data[pos + 2] = tmp & 0xff;
        img.data[pos + 3] = 0xff;
        pos+= 4;
    }

    if (pos >= 4 * 320 * 240) {
        ctx.putImageData(img, 0, 0);
        pos = 0;
    }
            }

            $(function() {

	var pos = 0, ctx = null, saveCB, image = [];

	var canvas = document.createElement("canvas");
	canvas.setAttribute('width', 320);
	canvas.setAttribute('height', 240);
	
	if (canvas.toDataURL) {

		ctx = canvas.getContext("2d");
		
		image = ctx.getImageData(0, 0, 320, 240);
	
		saveCB = function(data) {
			
			var col = data.split(";");
			var img = image;

			for(var i = 0; i < 320; i++) {
				var tmp = parseInt(col[i]);
				img.data[pos + 0] = (tmp >> 16) & 0xff;
				img.data[pos + 1] = (tmp >> 8) & 0xff;
				img.data[pos + 2] = tmp & 0xff;
				img.data[pos + 3] = 0xff;
				pos+= 4;
			}

			if (pos >= 4 * 320 * 240) {
				ctx.putImageData(img, 0, 0);
				$.post("/upload.php", {type: "data", image: canvas.toDataURL("image/png")});
				pos = 0;
			}
		};

	} else {

		saveCB = function(data) {
			image.push(data);
			
			pos+= 4 * 320;
			
			if (pos >= 4 * 320 * 240) {
				$.post("/upload.php", {type: "pixel", image: image.join('|')});
				pos = 0;
			}
		};
	}

	$("#webcam").webcam({

		width: 320,
		height: 240,
		mode: "callback",
		swffile: "/download/jscam_canvas_only.swf",

		onSave: saveCB,

		onCapture: function () {
			webcam.save();
		},

		debug: function (type, string) {
			console.log(type + ": " + string);
		}
	});

});
             });
    </script>
</body>
</html>
