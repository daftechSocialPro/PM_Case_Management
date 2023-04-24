import 'package:daf_project1/ui/widgets/custom_input_field.dart';
import 'package:daf_project1/ui/widgets/primary_button.dart';
import 'package:daf_project1/viewmodels/login_viewmodel.dart';
import 'package:daf_project1/viewmodels/settings_viewmodel.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

class SettingsPage extends StatelessWidget {
  const SettingsPage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    SettingsViewModel settingsViewModel = Provider.of<SettingsViewModel>(context, listen: false);
    settingsViewModel.getSettings();
    return Container(
      padding: EdgeInsets.symmetric(vertical: 16, horizontal: 32),
      child: Column(
        mainAxisSize: MainAxisSize.min,
        children: [
          CustomInputField(
              label: "IpAddress",
              errorMsg: '',
              controller: settingsViewModel.ipAddressController),
          // todo: Change TextEditingController
          CustomInputField(
              label: "Publish name",
              errorMsg: '',
              controller: settingsViewModel.publishNameController),
          CustomInputField(
              label: "Port number",
              isNumber: true,
              errorMsg: '',
              controller: settingsViewModel.portNumberController),
          // todo: change TextEditingController
          SizedBox(height: 20,),
          PrimaryButton(
            onPressed: () {
              settingsViewModel.saveSettings();
              Navigator.pop(context);
            },
            buttonText: 'Save',
            buttonState: ButtonState.idle,
          ),
        ],),
    );
  }
}
