
import 'dart:io';

import 'package:daf_project1/models/new_progress.dart';
import 'package:daf_project1/services/api.dart';
import 'package:daf_project1/services/file_picker.dart';
import 'package:daf_project1/viewmodels/ui_state.dart';
import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:geolocator/geolocator.dart';


class RequestTerminationViewModel
    extends ChangeNotifier{


  late final  Api _api;
  late TextEditingController reasonFieldController;
  late TextEditingController detailsFieldController;
  late final  FilePickerUtil _filePickerUtil;
  late File? selectedFile;
  late bool isUploaded;
  late String selectedFileName;
  late UiState terminationRequestUiState;

  RequestTerminationViewModel(Api api, FilePickerUtil filePickerUtil){
    _api = api;
    _filePickerUtil = filePickerUtil;
    selectedFileName = "";
    terminationRequestUiState = UiState.initial;
    reasonFieldController = new TextEditingController();
    detailsFieldController = new TextEditingController();

  }


  void addFile() async{
    this.selectedFile = await _filePickerUtil.getFile();
    if(this.selectedFile != null){
      selectedFileName = this.selectedFile!.path.split('/').last;
    }
      print("Selected File " + selectedFileName);
      notifyListeners();
  }
  void setUiState(UiState newValue){
    terminationRequestUiState = newValue;
    notifyListeners();
  }
  Future<void> requestTermination(String activityId) async{
    setUiState(UiState.onLoading);
    String employeeId = _api.user!.id;
    String reason = reasonFieldController.text.trim() + " " + detailsFieldController.text.trim();
    try{
      var result =  await _api.uploadTerminationRequest(file: selectedFile, activityId: activityId, employeeId: employeeId
      ,terminationReason: reason);
      print("After Termination request is uploaded");
      setUiState(UiState.onResult);
    }catch(e){
      setUiState(UiState.onError);
    }
  }


}
