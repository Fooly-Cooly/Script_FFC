exec("./ffc_download.gui");

function FFC_MinifigYesNo()
{
	%title = "FFC - Minifig/Beta Installation";
	%msg   = "Model files will be placed in\n"@
			 "\"base\\data\\shapes\\player\"\n"@
			 "This is done to allow for decal capability.\n"@
			 "Do you wish to proceed?";
	messageBoxYesNo(%title,%msg,"FFC_DownloadMinifig();","");
}

function FFC_DownloadMinifig()
{
	%this = new TCPObject(FFC_MinifigDownload)
	{
		port = 80;												//Port to use to connect server
		serv = "dl.dropboxusercontent.com";						//Server ip or host name
		path = "/u/26397357/Releases/Blockland/FFC/DL/";		//Server Path to files
		save = "base/data/shapes/player/";						//Download location for files
		total = 24;												//Number of files
		count = 0;												//Starting Point for download loop (Don't Change)
		len = "";												//Length of file to download (Don't Change)
		file0 = "minifig.dts";
		file1 = "minifig_activate.dsq";
		file2 = "minifig_armAttack.dsq";
		file3 = "minifig_armReadyBoth.dsq";
		file4 = "minifig_armReadyLeft.dsq";
		file5 = "minifig_armReadyRight.dsq";
		file6 = "minifig_back.dsq";
		file7 = "minifig_crouch.dsq";
		file8 = "minifig_crouchBack.dsq";
		file9 = "minifig_crouchRun.dsq";
		file10 = "minifig_crouchSide.dsq";
		file11 = "minifig_fall.dsq";
		file12 = "minifig_headside.dsq";
		file13 = "minifig_headup.dsq";
		file14 = "minifig_look.dsq";
		file15 = "minifig_root.dsq";
		file16 = "minifig_run.dsq";
		file17 = "minifig_side.dsq";
		file18 = "minifig_sit.dsq";
		file19 = "minifig_spearready.dsq";
		file20 = "minifig_spearThrow.dsq";
		file21 = "minifig_standjump.dsq";
		file22 = "minifig_talk.dsq";
		file23 = "mBeta.dts";
	};
	//---Checks if all files have valid names and don't already exist
	for(%i=0;%i<%this.total;%i++)
	{
		%file = %this.save @ %this.file[%i];
		%check = isWriteableFilename(%file) + isFile(%file);
		switch(%check)
		{
			case 0: %this.ErrorCallBack(2);return;
			case 2: fileDelete(%file);
		}
	}
	//---Initializes connection to server 
	%this.connect(%this.serv @ ":" @ %this.port);
}

function  FFC_MinifigDownload::errorCallBack(%this,%type)
{
	switch(%type)
	{
		case 0: %msg = "FFC Minifig: DNS Fail";
		case 1: %msg = "FFC Minifig: Connection Fail";
		case 2: %msg = "FFC Minifig: Not Writeable Filename";
	}
	echo(%msg);
	%this.delete();
}

function FFC_MinifigDownload::onDNSFailed(%this){%this.errorCallBack(0);}
function FFC_MinifigDownload::onConnectFailed(%this){%this.errorCallBack(1);}
function FFC_MinifigDownload::onConnected(%this)
{
	//---Shows/Sets gui info defaults
	canvas.pushDialog(FFC_DownloadGui);
	FFC_DownloadProgress.setValue(0);
	FFC_DownloadPText.setText("Loading...");
	FFC_DownloadTextT.setText("Done:   0KB");
	FFC_DownloadTextB.setText("Speed: N/A");
	FFC_DownloadWindow.setText("Downloading" SPC %this.count @ "/" @ %this.total SPC %this.file[%this.count]);
	//---Used for download speed
	%this.time = getSimTime();
	//---Request file from server
	%this.send("GET" SPC %this.path @ %this.file[%this.count] SPC "HTTP/1.0\r\nHost:" SPC %this.serv @ "\r\n\r\n");
}

function FFC_MinifigDownload::onLine(%this,%line)
{
	if(strPos(%line,"Content-Length:") > -1)
		%this.len = getWords(%line,1,5);
	else if(%line $= "")
		%this.setBinarySize(%this.len);
}

function FFC_byteRound(%type,%bytes)
{
	switch$(%type)
	{
		//---Rounds the number of bytes to the specified measurement	
		case "kb":	%result = mFloatLength(%bytes/1024, 2) @ "KB";
		case "mb":	%result = mFloatLength(%bytes/1048576, 2) @ "MB";
		//---Used to determine best measurement
		case "algm":%result = %bytes > 1048576 ? 1 : 0;
	}
	return %result;
}

function FFC_MinifigDownload::onBinChunk(%this,%chunk)
{
	//---Sets gui info as the file is downloaded
	%algm = FFC_byteRound("algm",%this.len) ? "mb" : "kb";
	FFC_DownloadProgress.setValue(%chunk/%this.len);
	FFC_DownloadPText.setText("Downloaded: " @ mFloor((%chunk/%this.len)*100) @ "%");
	FFC_DownloadTextB.setText("Speed: " @ mFloatLength(%chunk/(getSimTime()-%this.time),2) @ "kb/s");
	FFC_DownloadTextT.setText("Done:  " @ FFC_byteRound(%algm,%chunk) @ "/" @ FFC_byteRound(%algm,%this.len));
	//---Stops the function if download isn't complete
	if(%chunk < %this.len)
		return;
	//---Writes file to specified download location
	%this.saveBufferToFile(%this.save @ %this.file[%this.count]);
	//---Delays next file to allow current file to be saved
	%this.schedule(1000,nextFile);
}

function FFC_MinifigDownload::nextFile(%this)
{
	//---Starts download of next file, otherwise deletes connection and closes gui
	if(%this.count != %this.total-1)
	{
		%this.len = "";
		%this.count++;
		%this.setBinary(0);
		%this.disconnect();
		%this.connect(%this.serv @ ":" @ %this.port);
	}
	else
	{
		%this.delete();
		canvas.schedule(500,popDialog,FFC_DownloadGui);
		$pref::FFC::Minifig = 1;
	}
}
schedule(100,0,"FFC_MinifigYesNo");