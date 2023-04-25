import 'package:daf_project1/models/activity.dart';
import 'package:daf_project1/models/activity_progress.dart';
import 'package:daf_project1/services/api.dart';
import 'package:flutter/foundation.dart';

class ActivityApprovalViewModel extends ChangeNotifier{

  late Api _api;
  late String _userId;
  late Activity activity;
  late ActivityProgress progress;

  ActivityApprovalViewModel(Api api){
    this._api = api;
    _userId = api.user!.id;
  }

  Future<String> approve() async{

    return "";
  }

  bool isEmployeeCoordinator(){
    return _userId == progress.coordinatorId;
  }

  bool isEmployeeFinance(){
    return _userId == progress.financeId;
  }

  bool isEmployeeDirector(){
    return _userId == progress.directorId;
  }

  Future<String> discard() async{

    return "";
  }

  String getRelativeFilePath(){
    return _api.getRelativeFilePath();
  }

  void init({required Activity activity, required ActivityProgress progress}) {
    this.progress = progress;
    this.activity = activity;
  }

  void setApprovalStatus(ApprovalStatus type, bool value ,String remark){
    print("Type : $type} AND Value : $value ");
    _api.sendApprovalStatus(progress.id, value?0:1,type,remark);
    switch(type){
      case ApprovalStatus.finance:
         progress.isApprovedByFinance = value?1:0;
         progress.financeRemark = remark;

         break;
      case ApprovalStatus.director:
        progress.isApprovedByDirector = value? 1:0;
        progress.directorRemark= remark;
        break;
      case ApprovalStatus.coordinator:
        progress.isApprovedByCoordinator = value ? 1: 0;
        progress.cordinatorRemark = remark;
        break;
    }
    notifyListeners();

  }
  Future<void> downloadFile(String? path , String name) async{
    _api.requestDownload(path, name);
  }



}



enum ApprovalStatus{
  finance,
  coordinator,
  director
}