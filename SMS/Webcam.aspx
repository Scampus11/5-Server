<!doctype html>
<html>
<head>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>How to capture picture from webcam with Webcam.js</title>

</head>
</head>
<body>
    <!-- CSS -->
    <style>
    #my_camera{
        width: 640px;
        height: 480px;
        border: 1px solid black;
    }
	</style>

	<div class="modal" id="ModalWebcamPopup" style="display:block;">
        <div style="margin-top: -5px!important;max-width:752px;margin: 1.75rem auto;">
            <div class="modal-content" >
                <div class="modal-header">
                    <h4 class="modal-title" style="margin-right: 183px">Select Upload File</h4>
                    
                </div>
                <div class="PhotoUploadWrapper">
                    <div class="PhotoUpoloadCoseBtn">
                    </div>
                    <div class="PhotoUploadContent">
                        <div>

                            <a id="aback" style="float: left; font-family: Verdana, Geneva, sans-serif; font-size: 14px; line-height: 35px; text-indent: 18px; font-weight: bold; color: white">Close</a>
                        </div>
                        <div id="divpreviw">
                            <div class="PhotoUpoloadRightHeader">
                                <p style="float: left; font-family: Verdana, Geneva, sans-serif; font-size: 14px; line-height: 35px; text-indent: 18px; font-weight: bold; color: #FFF;">
                                    Camera Preview 
                                </p>

                            </div>

                            <div class="PhotoUpoloadLeftMainCont" style="text-align: center;">
                                <div>
                                    <div style="padding: 0px 0px 0px 0px; text-align: -webkit-center;">
                                        <div id="my_camera"></div>
                                    </div>
                                </div>
                                <br />
                                <div style="text-align: center;">
                                    <a onclick="take_snapshot()"><i class="material-icons icon-image-preview" style="color: black; font-size: 60px;" title="Capture Photo">camera</i>
                                    </a>

                                </div>
                            </div>
                        </div>
                        <div id="divCaptureImages">
                            <div class="PhotoUpoloadLeftHeader">
                                <p style="float: left; font-family: Verdana, Geneva, sans-serif; font-size: 14px; line-height: 35px; text-indent: 18px; font-weight: bold; color: #FFF;">
                                    Capture Photo
                                </p>

                            </div>

                            <div>
                                <div style="padding: 0px 0px 0px 0px; text-align: -webkit-center;">
                                    <div id="results"></div>
                                </div>
                            </div>
                            <br />
                            <div style="text-align: center;">
                                <a id="apreview"><i class="material-icons icon-image-preview" style="color: black; font-size: 60px;" title="Preview Photo">camera_alt</i>
                                </a>
                                <a onclick="Save()">
                                    <i class="material-icons icon-image-preview" style="color: black; font-size: 60px;" title="Save">save</i>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    </div></div>
	<input type=button value="Take Snapshot" onClick="take_snapshot()">
	
    <div id="results" ></div>
	
	<!-- Webcam.min.js -->
    <script type="text/javascript" src="webcamjs/webcam.min.js"></script>

	<!-- Configure a few settings and attach camera -->
	<script language="JavaScript">
		Webcam.set({
			width: 640,
			height: 480,
			image_format: 'jpeg',
			jpeg_quality: 90
		});
		Webcam.attach( '#my_camera' );
	</script>
	<!-- A button for taking snaps -->
	
	<!-- Code to handle taking the snapshot and displaying it locally -->
	<script language="JavaScript">

		function take_snapshot() {
			
			// take snapshot and get image data
			Webcam.snap( function(data_uri) {
				// display results in page
				document.getElementById('results').innerHTML = 
					'<img src="'+data_uri+'"/>';
			} );
		}
	</script>
	
</body>
</html>
