import 'package:daf_project1/models/activity_progress.dart';
import 'package:daf_project1/ui/widgets/custom_subtitle.dart';
import 'package:daf_project1/utils/date_formatter.dart';
import 'package:flutter/material.dart';

import 'download_file_item.dart';

class ActivityProgressItem extends StatelessWidget {
  final ActivityProgress progress;
  ActivityProgressItem({required this.progress, Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Container(
      margin: EdgeInsets.symmetric(vertical: 4),
      child: Card(
        elevation: 2,
        child: Padding(
          padding: EdgeInsets.all(16),
          child: Column(
            children: [
              Wrap(
                children: [
                  SubtitleText("Approvals"), SizedBox(width: 20,),
                _buildApprovalIndicator(context,label: "Finance", isApproved: progress.isApprovedByFinance),
                SizedBox(width: 16,)
                ,_buildApprovalIndicator(context,label: "Coordinator",isApproved: progress.isApprovedByCoordinator),
                  SizedBox(width: 16,)
                  ,_buildApprovalIndicator(context,label: "Director",isApproved: progress.isApprovedByDirector),
                ],),
              _buildCustomListTile("Actual Budget", "${progress.actualBudget} Birr"),
              _buildCustomListTile("Actual Worked", "${progress.actualWorked}"),
             /* DownloadItem(isFileExist: progress.documentPath != null, label : "Finance Doc", onPressed: ()=>{}, fileName: "${progress.financeDocPath ?? "No file attached"}"),
              DownloadItem(isFileExist: progress.documentPath != null, label : "Attached Doc", onPressed: ()=>{}, fileName: "${progress.documentPath ?? "No file attached"}"),
              */
              Divider(thickness: 0.5,),
              Row(
                children: [Text("Submitted By:"), SizedBox(width: 16,),Text("${progress.employee.fullName.toString()}")],
              ),
             SizedBox(height: 4,) ,
              Row(
                children: [Text("Submitted At:"),SizedBox(width: 16,),Text("${DateFormatter.getVerboseDateTimeRepresentation(progress.submissionTime)}")],
              )
            ],
          ),
        ),
      ),
    );
  }

  Widget _buildCustomListTile(String title, String trailing){
    return Padding(
      padding: const EdgeInsets.symmetric(vertical: 8.0),
      child: Row(
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        crossAxisAlignment: CrossAxisAlignment.center,
        children: [
          SubtitleText(title), Text(trailing)
        ],),
    );
  }

  Widget _buildApprovalIndicator(BuildContext context, {required String label,required int isApproved}){
    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 16.0),
      child: Row(
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        children: [Text(label), SizedBox(width: 8,),
          Icon(isApproved ==0 ? Icons.check_circle_outline  : Icons.cancel_outlined,
            color: isApproved==0 ?  Theme.of(context).primaryColor: Theme.of(context).errorColor,
          ),],
      ),
    );
  }
}
