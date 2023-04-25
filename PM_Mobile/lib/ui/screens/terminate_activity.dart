import 'package:daf_project1/ui/widgets/add_file_button.dart';
import 'package:daf_project1/ui/widgets/custom_input_field.dart';
import 'package:daf_project1/ui/widgets/primary_button.dart';
import 'package:daf_project1/viewmodels/request_termination_viewmodel.dart';
import 'package:daf_project1/viewmodels/ui_state.dart';
import 'package:flutter/material.dart';
import 'package:flutter/scheduler.dart';
import 'package:provider/provider.dart';

class TerminateActivity extends StatelessWidget {
  TerminateActivity({required this.activityId, Key? key}) : super(key: key);
  final String activityId;
  @override
  Widget build(BuildContext context) {
    RequestTerminationViewModel requestTerminationViewModel = Provider.of<RequestTerminationViewModel>(context, listen: false);

    return Scaffold(
        appBar: AppBar(
          title: Text("Termination Request"),
        ),
        body: Container(
          padding: EdgeInsets.only(bottom: 16, left: 16, right: 16, top: 16),
          child: Consumer<RequestTerminationViewModel>(
            builder: (context, value,_){
              if(value.terminationRequestUiState == UiState.onLoading){
                return Column(
                  crossAxisAlignment: CrossAxisAlignment.center,
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                  CircularProgressIndicator(),
                  SizedBox(height: 16,),
                  Text("Uploading your request...")
                ],);
              }else{
                if(value.terminationRequestUiState == UiState.onError){
                  final snackBar = SnackBar(content: Text('Error occurred while uploading your request, Please try again.'));
                  ScaffoldMessenger.of(context).showSnackBar(snackBar);
                }
                else if(value.terminationRequestUiState == UiState.onResult){
                  SchedulerBinding.instance!.addPostFrameCallback((_) {
                    final snackBar = SnackBar(
                        content: Text('Your request is uploaded successfully...'));
                    ScaffoldMessenger.of(context)
                        .showSnackBar(snackBar).closed
                        .then((value) => ScaffoldMessenger.of(context).clearSnackBars());
                  });
                 
                }
                return Column(
                  mainAxisSize: MainAxisSize.max,
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    CustomInputField(
                      label: "Reason",
                      controller: requestTerminationViewModel.reasonFieldController,
                      errorMsg: "",
                      isNumber: true,
                    ),
                    CustomInputField(
                      label: "Details Description",
                      controller: requestTerminationViewModel.detailsFieldController,
                      errorMsg: "",
                      isMultiLine: true,
                    ),
                    AddFileButton(label: "Add Doc", onPressed: ()=>{
                      requestTerminationViewModel.addFile()
                    },),
                    Expanded(
                        child: Align(
                          alignment: AlignmentDirectional.bottomCenter,
                          child: PrimaryButton(
                            buttonState: ButtonState.idle,
                            onPressed: () {
                              requestTerminationViewModel.requestTermination(activityId);
                            },
                            buttonText: 'Terminate',
                          ),
                        ))
                  ],
                );
              }


            }
          ),
        )
    );
  }
}
