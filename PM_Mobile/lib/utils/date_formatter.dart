
import 'package:flutter/material.dart';
import 'package:intl/intl.dart';

class DateFormatter {


  static String formatDate(DateTime? date){
    print("FormatDate is called...");
    if(date != null) {
      String formatted =  DateFormat('MMM dd, yyyy').format(date);
      print("Formatted : " + formatted);
      return formatted;
    }else{
      return "Unknown";
    }
  }

}
