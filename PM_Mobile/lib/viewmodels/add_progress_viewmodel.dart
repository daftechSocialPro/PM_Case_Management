import 'dart:io';

import 'package:daf_project1/models/new_progress.dart';
import 'package:daf_project1/services/api.dart';
import 'package:daf_project1/services/file_picker.dart';
import 'package:daf_project1/viewmodels/ui_state.dart';
import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:geolocator/geolocator.dart';

class AddProgressViewModel extends ChangeNotifier {
  late final Api _api;
  late TextEditingController progressFieldController;
  late TextEditingController budgetFieldController;
  late TextEditingController remarkFieldController;
  late TextEditingController reasonFieldController;
  late TextEditingController detailsFieldController;
  late final FilePickerUtil _filePickerUtil;
  late List<File?> selectedFiles;
  File? selectedFile;
  late bool isUploaded;
  late List<String> selectedFilesName;
  late String selectedFileName;
  late UiState addProgressUiState;
  String errorMessage = '';
  String selectedQuarterId = "";
  AddProgressViewModel(Api api, FilePickerUtil filePickerUtil) {
    _api = api;
    _filePickerUtil = filePickerUtil;
    selectedFiles = [];
    selectedFilesName = [];
    selectedFileName = "";
    progressFieldController = new TextEditingController();
    budgetFieldController = new TextEditingController();
    remarkFieldController = new TextEditingController();
    addProgressUiState = UiState.initial;
  }
  void setQuarterId(String id){
    selectedQuarterId = id;

  }
  void addFile(bool isMultiFile) async {
    if (isMultiFile) {
      this.selectedFiles = await _filePickerUtil.getFiles();
      selectedFilesName = [];
      selectedFiles.forEach((element) {
        if (element != null) {
          selectedFilesName.add(element.path.split("/").last);
        }
      });
      print("Selected Files " + selectedFilesName.toString());
      print("Selected Files *** " + selectedFiles.toString());
    } else {
      this.selectedFile = await _filePickerUtil.getFile();
      if (this.selectedFile != null) {
        selectedFileName = this.selectedFile!.path.split('/').last;
      }
      print("Selected File " + selectedFileName);
    }
    notifyListeners();
  }

  void setUiState(UiState newValue) {
    addProgressUiState = newValue;
    notifyListeners();
  }

  void resetAddProgressUiState(){
    addProgressUiState = UiState.initial;
  }

  Future<void> addProgress(String activityId, {isFinalize = false}) async {
    if(selectedQuarterId != ""){
      setUiState(UiState.onLoading);
      String employeeId = _api.user!.id;
      double progress = double.parse(progressFieldController.text.trim());
      double budget = double.parse(budgetFieldController.text.trim());
      String remark = remarkFieldController.text.trim();
      try {
        Position currentPosition = await _determinePosition();
        double lat = currentPosition.latitude;
        double long = currentPosition.longitude;
        Progress newProgress = Progress(
            activityId,
            employeeId,
            remark,
            lat,
            long,
            progress,
            budget,
            isFinalize ? 1 : 0, selectedQuarterId);
        var result =
        await _api.uploadProgress(selectedFiles, selectedFile, newProgress);
        setUiState(UiState.onResult);
      } catch (e, stacktrace) {
        print("e:" + e.toString());
        print("Stacktrace : " + stacktrace.toString());
        errorMessage = 'Error occurred while uploading your progress, Please try again.';
        setUiState(UiState.onError);
      }
    }else{
      errorMessage = 'Please select quarter';
      setUiState(UiState.onError);
    }

  }

  Future<Position> _determinePosition() async {
    bool serviceEnabled;
    LocationPermission permission;

    // Test if location services are enabled.
    serviceEnabled = await Geolocator.isLocationServiceEnabled();
    if (!serviceEnabled) {
      // Location services are not enabled don't continue
      // accessing the position and request users of the
      // App to enable the location services.
      errorMessage = 'Location services are disabled.';
    }

    permission = await Geolocator.checkPermission();
    if (permission == LocationPermission.denied) {
      permission = await Geolocator.requestPermission();
      if (permission == LocationPermission.denied) {
        // Permissions are denied, next time you could try
        // requesting permissions again (this is also where
        // Android's shouldShowRequestPermissionRationale
        // returned true. According to Android guidelines
        // your App should show an explanatory UI now.
        errorMessage = 'Location permissions are denied';
      }
    }

    if (permission == LocationPermission.deniedForever) {
      // Permissions are denied forever, handle appropriately.

      errorMessage = 'Location permissions are permanently denied, we cannot request permissions.';
    }

    // When we reach here, permissions are granted and we can
    // continue accessing the position of the device.
    return await Geolocator.getCurrentPosition();
  }
}
