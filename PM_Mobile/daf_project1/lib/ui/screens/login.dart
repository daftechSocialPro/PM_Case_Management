import 'package:daf_project1/services/api.dart';
import 'package:daf_project1/services/shared_preferences.dart';
import 'package:daf_project1/ui/screens/settings.dart';
import 'package:daf_project1/ui/widgets/custom_input_field.dart';
import 'package:daf_project1/ui/widgets/primary_button.dart';
import 'package:daf_project1/viewmodels/login_viewmodel.dart';
import 'package:daf_project1/viewmodels/settings_viewmodel.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../../locator.dart';

class LoginPage extends StatelessWidget {
   LoginPage({Key? key}) : super(key: key);
  final api = locator<Api>();
  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(
          title: Text("Login"),
        ),
        body: SingleChildScrollView(
          child: Padding(
            padding: const EdgeInsets.all(16.0),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.end,
              children: [
                IconButton(
                  icon: Icon(Icons.settings),
                  onPressed: (){
                    showModalBottomSheet(
                        context: context,
                        isScrollControlled: true,
                        builder:(context){
                          return Padding(
                              padding: EdgeInsets.only(bottom: MediaQuery.of(context).viewInsets.bottom),
                              child: ChangeNotifierProvider<SettingsViewModel>(
                                  create: (_)=>SettingsViewModel(locator<SharedPreferencesUtil>()),
                                  child: SettingsPage()));
                        },
                        shape:  RoundedRectangleBorder(borderRadius: BorderRadius.only(topLeft: Radius.circular(10.0),
                            topRight: Radius.circular(10.0))));},
                ),
                Container(
                  height: MediaQuery.of(context).size.height * 0.6,
                  padding: const EdgeInsets.symmetric(horizontal: 32.0),
                  child: Column(
                    mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                    children: [
                      Row(
                        mainAxisAlignment: MainAxisAlignment.center,
                        crossAxisAlignment: CrossAxisAlignment.center,
                        children: [
                          Icon(Icons.menu),
                          SizedBox(
                            width: 8,
                          ),
                          Text("IPDCS", style: Theme.of(context).textTheme.headline6,)
                        ],
                      ),
                      Column(
                        children: [
                          CustomInputField(
                              label: "Username",
                              errorMsg: '',
                              controller: TextEditingController()),
                          // todo: Change TextEditingController
                          CustomInputField(
                              label: "Password",
                              obscureText: true,
                              errorMsg: '',
                              controller: TextEditingController()),
                          // todo: change TextEditingController
                          SizedBox(
                            height: 20,
                          ),
                          PrimaryButton(
                            onPressed: () {
                              api.login('besha', 'password');
                              /*Navigator.of(context)
                                  .pushNamedAndRemoveUntil('/home', (Route<dynamic> route) => false);*/
                            },
                            buttonText: 'Login',
                            buttonState: ButtonState.idle,
                          ),
                        ],
                      ),
                    ],
                  ),
                ),
              ],
            ),
          ),
        ));
  }
}
