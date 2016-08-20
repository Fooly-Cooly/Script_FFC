datablock ShapeBaseImageData(ShellHatImage)
{
	shapeFile = "./hat_shell.dts";
	emap = false;
	doColorShift = True;
	colorshiftcolor = "0.9 0.9 0.9 1";
	mountPoint = 5;
	offset = "0 0 0.36";
	eyeOffset = "-1 -1 -1";
};

datablock ShapeBaseImageData(MajoraMaskImage)
{
	shapeFile = "./hat_majora.dts";
	emap = true;
	mountPoint = 5;
	offset = "0 0.5 0";
	eyeOffset = "-1 -1 -1";
};

datablock ShapeBaseImageData(BeakImage)
{
	shapeFile = "./acc_beak.dts";
	emap = false;
	doColorShift = True;
	colorshiftcolor = "1 0.7 0 1";
	mountPoint = 5;
	offset = "0 0.37 -0.19";
	eyeOffset = "-1 -1 -1";
};

FFC_AddHead("Beak", "Acc", "BeakImage");
FFC_AddHead("Majora", "Hat", "MajoraMaskImage");
FFC_AddHead("Shell", "Hat", "ShellHatImage");