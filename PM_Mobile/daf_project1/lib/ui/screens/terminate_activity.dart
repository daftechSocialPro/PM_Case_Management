import 'package:daf_project1/ui/widgets/add_file_button.dart';
import 'package:daf_project1/ui/widgets/custom_input_field.dart';
import 'package:daf_project1/ui/widgets/custom_subtitle.dart';
import 'package:daf_project1/ui/widgets/download_file_item.dart';
import 'package:daf_project1/ui/widgets/primary_button.dart';
import 'package:daf_project1/viewmodels/login_viewmodel.dart';
import 'package:flutter/material.dart';

class TerminateActivity extends StatelessWidget {
  const TerminateActivity({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(
          title: Text("Terminate"),
        ),
        body: Container(
          padding: EdgeInsets.only(bottom: 16, left: 16, right: 16, top: 16),
          child: Column(
            mainAxisSize: MainAxisSize.max,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              CustomInputField(
                label: "Reason",
                controller: TextEditingController(),
                errorMsg: "",
                isNumber: true,
              ),
              CustomInputField(
                label: "Details Description",
                controller: TextEditingController(),
                errorMsg: "",
                isMultiLine: true,
              ),
              AddFileButton(label: "Add Doc", selectMultipleFiles: true),
              Expanded(
                  child: Align(
                    alignment: AlignmentDirectional.bottomCenter,
                    child: PrimaryButton(
                      buttonState: ButtonState.idle,
                      onPressed: () {},
                      buttonText: 'Terminate',
                    ),
                  ))
            ],
          ),
        )
    );
  }
}
