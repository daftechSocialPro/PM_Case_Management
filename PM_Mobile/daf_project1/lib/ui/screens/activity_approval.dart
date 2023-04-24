import 'package:daf_project1/ui/widgets/custom_outlined_button.dart';
import 'package:daf_project1/ui/widgets/custom_subtitle.dart';
import 'package:daf_project1/ui/widgets/download_file_item.dart';
import 'package:daf_project1/ui/widgets/linear_progress_indicator.dart';
import 'package:daf_project1/ui/widgets/primary_button.dart';
import 'package:daf_project1/ui/widgets/progress_indicator.dart';
import 'package:daf_project1/viewmodels/login_viewmodel.dart';
import 'package:flutter/material.dart';

class ActivityApproval extends StatelessWidget {
  const ActivityApproval({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(
          title: Text("Approval"),
        ),
        body: Container(
          padding: EdgeInsets.symmetric(vertical: 16),
          child: Column(
            children: [
              Padding(
                padding: const EdgeInsets.symmetric(horizontal: 16.0),
                child: Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: [
          SubtitleText("Progress So far",),
                  CustomLinearProgressIndicator(percentile: 90.234)
          ]),
              ),
              ListTile(
                trailing: Text("18000 Birr"),
                title: Text("Budget used so far:"),
              ),
              ListTile(
                trailing: Text("5%"),
                title: Text("Current Progress:"),
              ),
              ListTile(
                trailing: Text("3000 Birr"),
                title: Text("Current Budget used:"),
              ),
              ListTile(
                title: SubtitleText("Remark"),
                subtitle: Text("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Lorem mollis aliquam ut porttitor leo a. Dui vivamus arcu felis bibendum ut tristique et."),
              ),
              Padding(
                padding: const EdgeInsets.symmetric(vertical : 8.0, horizontal: 16),
                child: DownloadItem(isFileExist: true, onPressed: ()=>{},label: "File Attached", fileName: "Report2.pdf"),
              ),
              Expanded(
                child: Align(
                    alignment: Alignment.bottomCenter,
                    child: Row(
                      mainAxisAlignment: MainAxisAlignment.center,
                      children: [
                        PrimaryButton(
                            onPressed: () => {},
                            buttonText: "Approve",
                            buttonState: ButtonState.idle),
                        SizedBox(width: 16,),
                        CustomOutlinedButton(onPressed: (){},buttonText: "Discard", buttonState: ButtonState.idle,)
                      ],
                    )),
              )
            ],
          ),
        )
    );
  }
}