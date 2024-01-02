//----------------------------------------------------------------------------
//
//  $Id: PreviewAndPrintLabel.js 10798 2010-01-16 22:38:52Z vbuzuev $ 
//
// Project -------------------------------------------------------------------
//
//  DYMO Label Framework
//
// Content -------------------------------------------------------------------
//
//  DYMO Label Framework JavaScript Library Samples: Preview and Print label
//
//----------------------------------------------------------------------------
//
//  Copyright (c), 2010, Sanford, L.P. All Rights Reserved.
//
//----------------------------------------------------------------------------


(function()
{
    // stores loaded label info
    var _label;

    // list of available layouts
    var _layoutFiles = [getLayout0(), getLayout1(), getLayout2(), getLayout3(), getLayout4()];
    var _layouts = null;

    // list of available photo files
    var _photoFiles = ["Photos/photo0.png", "Photos/photo2.jpg"];
    var Xml = '<?xml version="1.0" encoding="utf-8"?>\
<DieCutLabel Version="8.0" Units="twips">\
	<PaperOrientation>Landscape</PaperOrientation>\
	<Id>WhiteNameBadge11356</Id>\
	<IsOutlined>false</IsOutlined>\
	<PaperName>11356 White Name Badge - virtual</PaperName>\
	<DrawCommands>\
		<RoundRectangle X="0" Y="0" Width="2340" Height="5040" Rx="270" Ry="270" />\
	</DrawCommands>\
	<ObjectInfo>\
		<ImageObject>\
			<Name>Image</Name>\
			<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			<LinkedObjectName />\
			<Rotation>Rotation0</Rotation>\
			<IsMirrored>False</IsMirrored>\
			<IsVariable>False</IsVariable>\
			<GroupID>-1</GroupID>\
			<IsOutlined>False</IsOutlined>\
			<Image>iVBORw0KGgoAAAANSUhEUgAAAZAAAAE6AQAAAAAQQznpAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAUWSURBVGje7dpNlqQoEADg4LFwMQuPwFE4GhzNo3gEly582uVvAvFD2GbWdM9UbOql5GcRQJKACQsZM3xFoMtoEmELoydwhZYAiIa41qXEasgAWbR1MkERvkpiSUyNjIDCVQgQIZOBIq1IKFFmA7VMcDY5iaD4NxmZgAnPko4jliMzsBEYMvCkZUjkiaHJCEJ4knQSaUgCYlBklIkjSCcTi8kMlQiIjDXiEOlqxCIC1SjJWCeuIH2dNAWpi2ucHWRSkHOcHWTQkDYjUUNsSmZQRUpGHdmbGbRNvMbezKBP5WxmuJHKMZpB3cRrbMmAahSfsSUDytGyx5YMaEfLHuEgg564g/R60hwk6onZyQw3ImxkvEPcRvo7pNlIvEPMSm6lsiYD91JZJwAgU/H8tONI8vpeIIZSS5ElDUSaL9JJAn8wCIIWx12NoGVhaWxJ8OKzzGclMXtNxSQRZpWffgZNTpqFiY4lnEjH4Urg4wQek6Aj8zeQoCPTv0Cmu8RnxP9JZLxLnI6MfwdJpwKnIe1/mkzZxDzszZi+qUFkLIlDJP0Sa3eSzLP9b5H2E2T4ALEUsRlpEOlE0mlIT5GeJc13EpMRWyVdRuJbiPkdEmvEaAjcIHZ/U344YZZ8ffXHEKiS9W94QOZPEEOSUC5iKZJHKPcrBSG2Jr5CJgVJw3IHqjxpSNJK2zVH7mRbaVMYyPs1wtazHIBH2IXfq5+jUnenLVqecJt1v7BH0NyRwMLkGdhkLEc8m0zLVcCzyawlZJ857l7bhMcfp1MFjUzYrXTP3oyq8vbJJRvGLnRbNuccjMMwd3M1MtD1Yjp5IWvWLMvCfi4CWTNfJ0WbHd/bzIA91s97oia/KD/mGFNyfjsy5Fw5puRcuQ8y2e64k2sBxsxXV/naaCatFkuS0/eDXBe4ieRF5o2kJxyKpzy94R4MmYU9buCOg3w2FHgy57V/ZRYUZOuoKX9JkqmoiYJc4+Xou6ubXJ244nXLkqFId66T/tUpe8S8ogKx7AVEurIeQ5XEsoGmsqYsyZc9Isk7kr5CP0k0OLtQIfnGSiQT7rizyTxDxrLByEsk8fgfc2TAFZ9xXUmypOWoRTLSE/0WZXJ0QjagiGsEyUZ6L5OIUz3zY57wLkTrXA0vE6cnMzE4rr4MIsmmxlkkk0Top9VEtRcqQUyKXyZEiQwSaSVSnMJ3EulJ0qNRhL9bG5LYG2SQCDUqqUZ5RnCnrYE7+BEhBzI18gCVMcTrySyQEdUgI+49ZJBJK5ClCCjH0esd/dsJ8euO62uxJLFKDEMMQeJ9AjKBG6RjyVwjgSVWT6Ya8e8gY424+6RFZPgAaRDpv4V0NWLfQWKNmPsEEIFPkFCQ+SPEF2T6CCl/2TV+C7k2e+wM8zqOqJLIkb5OmufktVFniVUT4EisE/OcXAXoWTqUN6uS+REJGUkK/GMycWTSEM8RZqGIyKghjiOtkgwswSVvIfQ2ISkBVGAfk44j2U+EOGKVJHIkPUrMCaCbESQ8JNkJc0bSAnJbLROnIjNLshPmvcoEoQ4iKoQ67kizBFxj6lClQoyK5MfoKYm4YSiSnZ/pSNKX+eOFvS0BN2XaMZOSJB0zKglxPJgWAFFimTYWiGHaWCDFA24V8XSDSaSlG0wi3JMSgRg6e4m8Vp564sjsRXIkM9wghkxFJOVjDg1p+KscAaK9asRSF2VCRoWY+wR+yBvIPlv9kB/yQ37I/5Z0fzn55+uty/ILW/dn46EoYQsAAAAASUVORK5CYII=</Image>\
			<ScaleMode>Uniform</ScaleMode>\
			<BorderWidth>0</BorderWidth>\
			<BorderColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<HorizontalAlignment>Left</HorizontalAlignment>\
			<VerticalAlignment>Top</VerticalAlignment>\
		</ImageObject>\
		<Bounds X="331" Y="113" Width="315" Height="210" />\
	</ObjectInfo>\
	<ObjectInfo>\
		<ShapeObject Stroke="SolidLine">\
			<Name>Line</Name>\
			<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<BackColor Alpha="255" Red="255" Green="255" Blue="255" />\
			<LinkedObjectName />\
			<Rotation>Rotation0</Rotation>\
			<IsMirrored>False</IsMirrored>\
			<IsVariable>False</IsVariable>\
			<GroupID>-1</GroupID>\
			<IsOutlined>False</IsOutlined>\
			<ShapeType>HorizontalLine</ShapeType>\
			<LineWidth>30</LineWidth>\
			<LineAlignment>Center</LineAlignment>\
			<FillColor Alpha="0" Red="255" Green="255" Blue="255" />\
		</ShapeObject>\
		<Bounds X="331" Y="358" Width="4618" Height="30" />\
	</ObjectInfo>\
	<ObjectInfo>\
		<ShapeObject Stroke="SolidLine">\
			<Name>Line1</Name>\
			<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<BackColor Alpha="255" Red="255" Green="255" Blue="255" />\
			<LinkedObjectName />\
			<Rotation>Rotation0</Rotation>\
			<IsMirrored>False</IsMirrored>\
			<IsVariable>False</IsVariable>\
			<GroupID>-1</GroupID>\
			<IsOutlined>False</IsOutlined>\
			<ShapeType>HorizontalLine</ShapeType>\
			<LineWidth>30</LineWidth>\
			<LineAlignment>Center</LineAlignment>\
			<FillColor Alpha="0" Red="255" Green="255" Blue="255" />\
		</ShapeObject>\
		<Bounds X="331" Y="778" Width="4618" Height="30" />\
	</ObjectInfo>\
	<ObjectInfo>\
		<TextObject>\
			<Name>Text</Name>\
			<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			<LinkedObjectName />\
			<Rotation>Rotation0</Rotation>\
			<IsMirrored>False</IsMirrored>\
			<IsVariable>False</IsVariable>\
			<GroupID>-1</GroupID>\
			<IsOutlined>False</IsOutlined>\
			<HorizontalAlignment>Center</HorizontalAlignment>\
			<VerticalAlignment>Middle</VerticalAlignment>\
			<TextFitMode>ShrinkToFit</TextFitMode>\
			<UseFullFontHeight>True</UseFullFontHeight>\
			<Verticalized>False</Verticalized>\
			<StyledText>\
				<Element>\
					<String xml:space="preserve">VISITOR ENTRY CARD</String>\
					<Attributes>\
						<Font Family="Arial" Size="20" Bold="True" Italic="False" Underline="False" Strikeout="False" />\
						<ForeColor Alpha="255" Red="0" Green="0" Blue="0" HueScale="100" />\
					</Attributes>\
				</Element>\
			</StyledText>\
		</TextObject>\
		<Bounds X="362" Y="418" Width="4559" Height="360" />\
	</ObjectInfo>\
	<ObjectInfo>\
		<TextObject>\
			<Name>Text1</Name>\
			<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			<LinkedObjectName />\
			<Rotation>Rotation0</Rotation>\
			<IsMirrored>False</IsMirrored>\
			<IsVariable>False</IsVariable>\
			<GroupID>-1</GroupID>\
			<IsOutlined>False</IsOutlined>\
			<HorizontalAlignment>Center</HorizontalAlignment>\
			<VerticalAlignment>Top</VerticalAlignment>\
			<TextFitMode>ShrinkToFit</TextFitMode>\
			<UseFullFontHeight>True</UseFullFontHeight>\
			<Verticalized>False</Verticalized>\
			<StyledText>\
				<Element>\
					<String xml:space="preserve">Smart Campus VMS</String>\
					<Attributes>\
						<Font Family="Arial" Size="20" Bold="False" Italic="False" Underline="False" Strikeout="False" />\
						<ForeColor Alpha="255" Red="0" Green="0" Blue="0" HueScale="100" />\
					</Attributes>\
				</Element>\
			</StyledText>\
		</TextObject>\
		<Bounds X="826" Y="120" Width="4107" Height="210" />\
	</ObjectInfo>\
	<ObjectInfo>\
		<TextObject>\
			<Name>Text2</Name>\
			<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			<LinkedObjectName />\
			<Rotation>Rotation0</Rotation>\
			<IsMirrored>False</IsMirrored>\
			<IsVariable>False</IsVariable>\
			<GroupID>-1</GroupID>\
			<IsOutlined>False</IsOutlined>\
			<HorizontalAlignment>Left</HorizontalAlignment>\
			<VerticalAlignment>Top</VerticalAlignment>\
			<TextFitMode>ShrinkToFit</TextFitMode>\
			<UseFullFontHeight>True</UseFullFontHeight>\
			<Verticalized>False</Verticalized>\
			<StyledText>\
				<Element>\
					<String xml:space="preserve">Musa Sallah</String>\
					<Attributes>\
						<Font Family="Arial" Size="10" Bold="False" Italic="False" Underline="False" Strikeout="False" />\
						<ForeColor Alpha="255" Red="0" Green="0" Blue="0" HueScale="100" />\
					</Attributes>\
				</Element>\
			</StyledText>\
		</TextObject>\
		<Bounds X="1693" Y="1083.50769042969" Width="3210" Height="234.492279052734" />\
	</ObjectInfo>\
	<ObjectInfo>\
		<BarcodeObject>\
			<Name>Barcode</Name>\
			<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			<LinkedObjectName>ID</LinkedObjectName>\
			<Rotation>Rotation0</Rotation>\
			<IsMirrored>False</IsMirrored>\
			<IsVariable>False</IsVariable>\
			<GroupID>-1</GroupID>\
			<IsOutlined>False</IsOutlined>\
			<Text>ABC123</Text>\
			<Type>Code39</Type>\
			<Size>Small</Size>\
			<TextPosition>Bottom</TextPosition>\
			<TextFont Family="Arial" Size="10" Bold="False" Italic="False" Underline="False" Strikeout="False" />\
			<CheckSumFont Family="Arial" Size="8" Bold="False" Italic="False" Underline="False" Strikeout="False" />\
			<TextEmbedding>None</TextEmbedding>\
			<ECLevel>0</ECLevel>\
			<HorizontalAlignment>Center</HorizontalAlignment>\
			<QuietZonesPadding Left="0" Top="0" Right="0" Bottom="0" />\
		</BarcodeObject>\
		<Bounds X="1606" Y="1731" Width="3329" Height="492" />\
	</ObjectInfo>\
	<ObjectInfo>\
		<ImageObject>\
			<Name>Image1</Name>\
			<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			<LinkedObjectName />\
			<Rotation>Rotation0</Rotation>\
			<IsMirrored>False</IsMirrored>\
			<IsVariable>False</IsVariable>\
			<GroupID>-1</GroupID>\
			<IsOutlined>False</IsOutlined>\
			<Image>iVBORw0KGgoAAAANSUhEUgAAAZAAAADLAQAAAABo+vjhAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAAH14AAB9eAX41B5MAAAUcSURBVGje7ZpBju4mDID9K4sso+7eomp6hLdspUrplbp7VRdh9C96rYzmAj0Co16ASpXKAkEhJMEQh5hKfaqql82fIfnANsYYZ8DRlwH66p27QuDqmq8QeYmMF4i5JLxkcD3Ig3hg5YNGLECnggyUkjSiYHBOQ0c9EzOJiLV18dYhupuAlGsIPyb+FJcegZQrdi8oA5iBQpZNCQ0T8bQHSq7dVKRkTwLRR5vsmEh6UVM2eyEQkaaQms1XEIe7bZdBXcueQqBEFFJAE2Z+PyMLNhNhZnlGsreWs5nVCdGZQVTHGCXX2AADEbldz8qcBDPF7MnhdhRVmFV3t0hpIntSppwXe5L91FAi+tTpaWZeUYQKei8n0VXpZm8ImfDqOi5TutkzRxSxQMqmPkcEsQyXfGCbkFFPXnliSRXK+AiDEUHFm0IZH8cQIumQmvcjJ4T8AGTcckvW6mNyQujtofBMv7QzBHdnDq0V4KvLkFyT4698SxswUoj1ctxlyOQyI+dapztEPFwFkYejoNBs5xqiUPzMHlwjOtk2n69rJFnZyZGH2KSB7nkI8i37YCJiJug6gtwx88wKopLJMs+sIEhp0/MQgyb9wUNwbBU8BIejZeYhaKtRIw9B4Rg5XB1JdrI9D5HITg8e8ifSX/AQnI/JmYXgrUNNLMSJpLQemUhS2gw8RKJV1vOQ35D+HXeUQ5y3Fx6iYBfHwo88RB/bh4GPMxOBdDczBdv1V5+SJe6QTf93lP9WkQV2/d+cGDhI2PE2/X9FcamGrFtR1HpAOWcNie0bktK/ChIOcduByY4oqlWQdSLjpmcmlJdUEBWmIqaBHrEcJEaLdQr1zEPimlwtpR3aY2rIqsZqKeVQjLlGbJzGNZj/wUPM5sZiCi7GQnbPD57CRBR8cLsyTETCd1Enr9LvPGSBT/HGb+bMUcSO+HSEZzHvlBviE2XeKOZAvEOGqbyffX0gfpvV8z1iQ9PPu1bh2Z3zx4PzT9tfsjfjjSfvWe73u4ydj/u6hui96aujC++bqrKQ7dGU+p2gh0qEEUfT0a8YJMB1HDvEgtTv0hmoBFhBIApGcR3G0SBJtNWEl5tFUUuL75lvAdJOWyJFMS0qYD4iW1zr8lgHjOroSUAlVdjOREOUMU6N98q/3DXiDmNJ+GVTRmcH2DOidxV8nFTRtOoGcbsQPk/ccqV7ZLvCEWSLy46LTPsBuQmJq+SVi4R0Oh7D2MiaTq5WfjYhIe67vhmxbUhI9u3QhvjXzdiG9MGRucjqJuF0Kec25IHP5LfI2gDZqcJxEDGbjo3oDSkLUTVkte0yFdU/BvJ1WZ2pIPEwIaGsGtVG2ZCej8SDsTpVsyqI3ZCJj8Qjy7kwV0PWWVen2lQNWfOJc633FjkXYWvIemKXYwsSAll20r9H1kP+4pqQoMazCQklEts3IWFt2aEJCetetyEhEVVjG7JM0dANiJ95MbchPhEnCqZVxIDpGhEH0LcigqpJ54goVq2E6QYJv5kkmipjYyTmYpjR3Q2y3yHB+jri87R+HQjVgqY6EvuXKD8mVcmQKEWqCxBfMEpkE0jsabQlPwtiBCf1cZ8gP4siJO3uOppaXhTxSYfZk+XhBjGnI8/Fl2wacZVBLj1ZAP3Nw905/7+AzM2I+geIa0ZkO/Lejry2I89m5Ju+GfnQjtD/zFBHpnZkbkYerhkZvyBfkBZk+h8h838VcZ8DebQjXTvSfxZkbEemdmRuRhoM5prlcu5v395e+m1+yVMAAAAASUVORK5CYII=</Image>\
			<ScaleMode>Uniform</ScaleMode>\
			<BorderWidth>0</BorderWidth>\
			<BorderColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<HorizontalAlignment>Left</HorizontalAlignment>\
			<VerticalAlignment>Top</VerticalAlignment>\
		</ImageObject>\
		<Bounds X="362" Y="1152" Width="1182" Height="1004" />\
	</ObjectInfo>\
	<ObjectInfo>\
		<TextObject>\
			<Name>Text_1</Name>\
			<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			<LinkedObjectName />\
			<Rotation>Rotation0</Rotation>\
			<IsMirrored>False</IsMirrored>\
			<IsVariable>False</IsVariable>\
			<GroupID>-1</GroupID>\
			<IsOutlined>False</IsOutlined>\
			<HorizontalAlignment>Left</HorizontalAlignment>\
			<VerticalAlignment>Top</VerticalAlignment>\
			<TextFitMode>ShrinkToFit</TextFitMode>\
			<UseFullFontHeight>True</UseFullFontHeight>\
			<Verticalized>False</Verticalized>\
			<StyledText>\
				<Element>\
					<String xml:space="preserve">Name:</String>\
					<Attributes>\
						<Font Family="Arial" Size="10" Bold="False" Italic="False" Underline="False" Strikeout="False" />\
						<ForeColor Alpha="255" Red="0" Green="0" Blue="0" HueScale="100" />\
					</Attributes>\
				</Element>\
			</StyledText>\
		</TextObject>\
		<Bounds X="1688.38989257813" Y="819.695373535156" Width="3215.11352539063" Height="239.102432250977" />\
	</ObjectInfo>\
	<ObjectInfo>\
		<TextObject>\
			<Name>Text4</Name>\
			<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			<LinkedObjectName />\
			<Rotation>Rotation0</Rotation>\
			<IsMirrored>False</IsMirrored>\
			<IsVariable>False</IsVariable>\
			<GroupID>-1</GroupID>\
			<IsOutlined>False</IsOutlined>\
			<HorizontalAlignment>Left</HorizontalAlignment>\
			<VerticalAlignment>Top</VerticalAlignment>\
			<TextFitMode>ShrinkToFit</TextFitMode>\
			<UseFullFontHeight>True</UseFullFontHeight>\
			<Verticalized>False</Verticalized>\
			<StyledText>\
				<Element>\
					<String xml:space="preserve">ID #</String>\
					<Attributes>\
						<Font Family="Arial" Size="8" Bold="False" Italic="False" Underline="False" Strikeout="False" />\
						<ForeColor Alpha="255" Red="0" Green="0" Blue="0" HueScale="100" />\
					</Attributes>\
				</Element>\
			</StyledText>\
		</TextObject>\
		<Bounds X="1708.00085449219" Y="1296" Width="375" Height="185.847686767578" />\
	</ObjectInfo>\
	<ObjectInfo>\
		<TextObject>\
			<Name>ID</Name>\
			<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			<LinkedObjectName />\
			<Rotation>Rotation0</Rotation>\
			<IsMirrored>False</IsMirrored>\
			<IsVariable>True</IsVariable>\
			<GroupID>-1</GroupID>\
			<IsOutlined>False</IsOutlined>\
			<HorizontalAlignment>Left</HorizontalAlignment>\
			<VerticalAlignment>Top</VerticalAlignment>\
			<TextFitMode>ShrinkToFit</TextFitMode>\
			<UseFullFontHeight>True</UseFullFontHeight>\
			<Verticalized>False</Verticalized>\
			<StyledText>\
				<Element>\
					<String xml:space="preserve">ABC123</String>\
					<Attributes>\
						<Font Family="Arial" Size="12" Bold="True" Italic="False" Underline="False" Strikeout="False" />\
						<ForeColor Alpha="255" Red="0" Green="0" Blue="0" HueScale="100" />\
					</Attributes>\
				</Element>\
			</StyledText>\
		</TextObject>\
		<Bounds X="1706.58361816406" Y="1483.19995117188" Width="1109.05517578125" Height="238.110244750977" />\
	</ObjectInfo>\
	<ObjectInfo>\
		<TextObject>\
			<Name>Expire_Date</Name>\
			<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			<LinkedObjectName />\
			<Rotation>Rotation0</Rotation>\
			<IsMirrored>False</IsMirrored>\
			<IsVariable>True</IsVariable>\
			<GroupID>-1</GroupID>\
			<IsOutlined>False</IsOutlined>\
			<HorizontalAlignment>Left</HorizontalAlignment>\
			<VerticalAlignment>Top</VerticalAlignment>\
			<TextFitMode>ShrinkToFit</TextFitMode>\
			<UseFullFontHeight>True</UseFullFontHeight>\
			<Verticalized>False</Verticalized>\
			<StyledText>\
				<Element>\
					<String xml:space="preserve">06/22/2010</String>\
					<Attributes>\
						<Font Family="Arial" Size="12" Bold="True" Italic="False" Underline="False" Strikeout="False" />\
						<ForeColor Alpha="255" Red="0" Green="0" Blue="0" HueScale="100" />\
					</Attributes>\
				</Element>\
			</StyledText>\
		</TextObject>\
		<Bounds X="3135.83959960938" Y="1488.08349609375" Width="1335.23620605469" Height="215.634582519531" />\
	</ObjectInfo>\
	<ObjectInfo>\
		<TextObject>\
			<Name>Text5</Name>\
			<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			<LinkedObjectName />\
			<Rotation>Rotation0</Rotation>\
			<IsMirrored>False</IsMirrored>\
			<IsVariable>False</IsVariable>\
			<GroupID>-1</GroupID>\
			<IsOutlined>False</IsOutlined>\
			<HorizontalAlignment>Left</HorizontalAlignment>\
			<VerticalAlignment>Top</VerticalAlignment>\
			<TextFitMode>ShrinkToFit</TextFitMode>\
			<UseFullFontHeight>True</UseFullFontHeight>\
			<Verticalized>False</Verticalized>\
			<StyledText>\
				<Element>\
					<String xml:space="preserve">Expires:</String>\
					<Attributes>\
						<Font Family="Arial" Size="8" Bold="False" Italic="False" Underline="False" Strikeout="False" />\
						<ForeColor Alpha="255" Red="0" Green="0" Blue="0" HueScale="100" />\
					</Attributes>\
				</Element>\
			</StyledText>\
		</TextObject>\
		<Bounds X="3133.0009765625" Y="1299.56030273437" Width="675" Height="190.45783996582" />\
	</ObjectInfo>\
</DieCutLabel>';
    

    // return list of available layouts, load them from file if nessesary
    function getLayouts()
    {
        if (_layouts == null)
        {
            // load layouts
            var result = new Array();
            var base = document.location.href;
            for (var i = 0; i < _layoutFiles.length; i++)
                result.push(dymo.label.framework.openLabelXml(_layoutFiles[i]));
            _layouts = result;
        }

        return _layouts;
    }

    // current data on the label. For simplicity we support one Text obje t and one Image object
    var _labelData = {  };

    // applies data to the label
    function applyDataToLabel(label, labelData)
    {
        var names = label.getObjectNames();

        for (var name in labelData)
            if (itemIndexOf(names, name) >= 0)
            label.setObjectText(name, labelData[name]);
    }

    // updates control states depend on available objects on the label
    function updateControls()
    {
        var selectPhotoButton = document.getElementById('selectPhotoButton');
        var labelName = document.getElementById('ContentPlaceHolder1_txtFirst_Name');
        var labelRegNo = document.getElementById('labelRegNo');
        var labelExpDate = document.getElementById('ContentPlaceHolder1_txtValidToDatetime');
        var labelBarcode = document.getElementById('labelBarcode');


        var names = _label.getObjectNames();

        selectPhotoButton.disabled = itemIndexOf(names, 'Image1') == -1;
        labelName.disabled = itemIndexOf(names, 'Text2') == -1;
        labelRegNo.disabled = itemIndexOf(names, 'ID') == -1;
        labelExpDate.disabled = itemIndexOf(names, 'Expire_Date') == -1;
        labelBarcode.disabled = itemIndexOf(names, 'Barcode') == -1;
    }

    /// replaces the last component of the url with fileName
    function replaceFileName(url, fileName)
    {
        var i = url.lastIndexOf('/');
        var r = url.substr(0, i + 1) + fileName;

        // fix for opera
        if (r.indexOf('file://localhost') == 0)
            r = r.replace('file://localhost', 'file://');

        return r;
    }

    // returns an index of an item in an array. Returns -1 if not found

    function itemIndexOf(array, item)
    {
        for (var i = 0; i < array.length; i++)
            if (array[i] == item) return i;

        return -1;
    }

    // loads the defualt layout at onload()

    function setupDefaultLayout()
    {
        _label = dymo.label.framework.openLabelXml(Xml);
        applyDataToLabel(_label, _labelData);
    }

    // updates label preview image
    // Generates label preview and updates corresponend <img> element
    // Note: this does not work in IE 6 & 7 because they don't support data urls
    // if you want previews in IE 6 & 7 you have to do it on the server side
    function updatePreview()
    {
        if (!_label)
            return;

        var pngData = _label.render();

        var labelImage = document.getElementById('labelImage');
        labelImage.src = "data:image/png;base64," + pngData;
    }

    // called when clicked on a photo
    function photoClick(e)
    {
        var overlay = document.getElementById('dialog-overlay');
        var wrapper = document.getElementById('dialog-wrapper');

        var targ;
        var ee = e;
        if (!ee)
            ee = window.event;

        if (ee.target)
        {
            targ = ee.target;
        }
        else if (ee.srcElement)
        {
            targ = ee.srcElement;
        }
        if (targ.nodeType == 3) // defeat Safari bug
        {
            targ = targ.parentNode;
        }

        // save selected photo
        var url = targ.src;
        if (url.indexOf('file://localhost') == 0)
            url = url.replace('file://localhost', 'file://');

        _labelData.Image1 = dymo.label.framework.loadImageAsPngBase64(url);

        // update label
        applyDataToLabel(_label, _labelData);
        updatePreview();

        // close dialog
        wrapper.style.display = "none";
        overlay.style.display = "none";
    }

    // set "dialog" caption 
    function dialogSetCaption(caption)
    {
        header = document.getElementById('dialog-header');

        // remove old caption
        while (header.firstChild)
            header.removeChild(header.firstChild);

        // set new caption
        header.appendChild(document.createTextNode(caption));
    }

    // event handler for selectPhotoButton.onclick event

    function selectPhotoButtonClick()
    {
        var overlay = document.getElementById('dialog-overlay');
        var wrapper = document.getElementById('dialog-wrapper');
        var content = document.getElementById('dialog-content');


        // remove old content

        while (content.firstChild)
            content.removeChild(content.firstChild);

        // add photos
        for (var i = 0; i < _photoFiles.length; i++)
        {
            var a = document.createElement('a');
            //a.setAttribute("class", "photo");
            a.className = "Image1";
            a.href = "javascript:void(0)";
            var img = document.createElement('img');
            //img.setAttribute("class", "photo");
            //img.className = "photo";
            img.src = replaceFileName(document.location.href, _photoFiles[i]);

            // this is to set the height to 100px. Just setting height is css does not work in IE
            //var height = 150;
            //img.width = img.width / img.height * height;
            //img.height = height;

            img.onclick = photoClick;
            //img.class = "photo";

            a.appendChild(img);
            content.appendChild(a);
        }

        // show dialog
        dialogSetCaption("Select Photo");
        overlay.style.display = "block";
        wrapper.style.display = "block";
    }

    // called when clicked on a layout
    function layoutClick(e)
    {
        var overlay = document.getElementById('dialog-overlay');
        var wrapper = document.getElementById('dialog-wrapper');

        var targ;
        var ee = e;
        if (!ee)
            ee = window.event;

        if (ee.target)
        {
            targ = ee.target;
        }
        else if (ee.srcElement)
        {
            targ = ee.srcElement;
        }
        if (targ.nodeType == 3) // defeat Safari bug
        {
            targ = targ.parentNode;
        }

        // update label

        _label = targ.labelLayout;
        updatePreview();
        updateControls();

        // close dialog
        wrapper.style.display = "none";
        overlay.style.display = "none";
    }

    // event handler for selectPhotoButton.onclick event

    function changeLayoutButtonClick()
    {
        var overlay = document.getElementById('dialog-overlay');
        var wrapper = document.getElementById('dialog-wrapper');
        var content = document.getElementById('dialog-content');

        // remove old content
        while (content.firstChild)
            content.removeChild(content.firstChild);

        // add layouts

        var layouts = getLayouts();
        for (var i = 0; i < layouts.length; i++)
        {
            // set layout data
            var layout = layouts[i];
            applyDataToLabel(layout, _labelData);

            var a = document.createElement('a');
            //a.setAttribute("class", "photo");
            a.className = "layout";
            a.href = "javascript:void(0)";
            var img = document.createElement('img');
            img.src = "data:image/png;base64," + layout.render();
            img.onclick = layoutClick;

            // remember the layout as well to update _label when clicked on it

            img.labelLayout = layout;

            a.appendChild(img);
            content.appendChild(a);
        }

        // add a dummy div to clear layout's float:left style

        var d = document.createElement('div');
        d.style.clear = "both";
        content.appendChild(d);

        // show dialog
        dialogSetCaption("Select Layout");
        overlay.style.display = "block";
        wrapper.style.display = "block";
    }

    // called when the document completly loaded

    function onload()
    {
        var labelFile = document.getElementById('labelFile');
        var labelName = document.getElementById('ContentPlaceHolder1_txtFirst_Name');
        var labelRegNo = document.getElementById('labelRegNo');
        var labelExpDate = document.getElementById('ContentPlaceHolder1_txtValidToDatetime');
        var labelBarcode = document.getElementById('labelBarcode');
        var printersSelect = document.getElementById('printersSelect');
        var printButton = document.getElementById('ContentPlaceHolder1_lnkprint');
        var selectPhotoButton = document.getElementById('selectPhotoButton');
        var changeLayoutButton = document.getElementById('changeLayoutButton');


        // initialize controls
        //printButton.disabled = true;
        //addressTextArea.disabled = true;

        if (_labelData.Text2)
            labelName.value = _labelData.Text2;
        alert(_labelData.Text2);
        // loads all supported printers into a combo box 
        function loadPrinters()
        {
            var printers = dymo.label.framework.getPrinters();
            if (printers.length == 0)
            {
                alert("No DYMO printers are installed. Install DYMO printers.");
                return;
            }

            for (var i = 0; i < printers.length; i++)
            {
                var printerName = printers[i].name;

                var option = document.createElement('option');
                option.value = printerName;
                option.appendChild(document.createTextNode(printerName));
                printersSelect.appendChild(option);
            }
        };

        // updates name on the label when user types in input field
        //labelName.onkeyup = function()
        //{
        //    if (!_label)
        //    {
        //        alert('Load label before entering text');
        //        return;
        //    }

        //    // set labelData

        //    _labelData.Text2 = labelName.value;
        //    applyDataToLabel(_label, _labelData);
        //    updatePreview();
        //}

        // updates Expiry Date on the label when user types in input field

        //labelExpDate.onkeyup = function () {
        //    if (!_label) {
        //        alert('Load label before entering text');
        //        return;
        //    }

        //    // set labelData

        //    _labelData.Expire_Date = labelExpDate.value;
        //    applyDataToLabel(_label, _labelData);
        //    updatePreview();
        //}

        // updates Reg No on the label when user types in input field
        //labelRegNo.onkeyup = function () {
        //    if (!_label) {
        //        alert('Load label before entering text');
        //        return;
        //    }

        //    // set labelData
        //    _labelData.ID = labelRegNo.value;
        //    applyDataToLabel(_label, _labelData);
        //    updatePreview();
        //}

        // updates name on the label when user types in input field
        //labelBarcode.onkeyup = function () {
        //    if (!_label) {
        //        alert('Load label before entering text');
        //        return;
        //    }

        //    // set labelData
        //    _labelData.Barcode = labelBarcode.value;
        //    applyDataToLabel(_label, _labelData);
        //    updatePreview();
        //}

        // prints the label
        //printButton.onclick = function()
        //{
        //    loadPrinters();
        //    if (!_label)
        //    {
        //        alert("Load label before printing");
        //        return;
        //    }

        //    //alert(printersSelect.value);
        //    _label.print(printersSelect.value);
        //}

        //selectPhotoButton.onclick = selectPhotoButtonClick;
        //changeLayoutButton.onclick = changeLayoutButtonClick;

        // onload() initialization
        //loadPrinters();
        setupDefaultLayout();
        updatePreview();
      //  updateControls();
    };

    function initTests()
	{
		if(dymo.label.framework.init)
		{
			//dymo.label.framework.trace = true;

			dymo.label.framework.init(onload);
		} else {
			onload();
		}
	}

    // register onload event

    if (window.addEventListener)
        window.addEventListener("load", initTests, false);
    else if (window.attachEvent)
        window.attachEvent("onload", initTests);
    else
        window.onload = initTests;

} ());