package com.example.casemanagment

import android.content.Context
import android.content.Intent
import android.content.SharedPreferences
import android.os.Bundle
import android.os.Message
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ArrayAdapter
import android.widget.EditText
import android.widget.TextView
import android.widget.Toast
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.example.casemanagment.Model.Messagess
import com.example.casemanagment.Model.TblUser
import com.example.casemanagment.Remote.AffairHistoryAdapter
import com.example.casemanagment.Remote.IMyAPI
import com.example.casemanagment.Remote.RetrofitClient
import com.google.android.material.bottomsheet.BottomSheetDialogFragment
import dmax.dialog.SpotsDialog
import io.reactivex.android.schedulers.AndroidSchedulers
import io.reactivex.disposables.CompositeDisposable
import io.reactivex.schedulers.Schedulers


class SendSMSFragment(val affairId :String?) : BottomSheetDialogFragment(){

    override fun onCreateView(
            inflater: LayoutInflater,
            container: ViewGroup?,
            savedInstanceState: Bundle?
    ): View? {

        val view = inflater.inflate(R.layout.fragment_send_s_m_s, container, false)
        return view
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        val sharedPref: SharedPreferences = this.requireActivity().getSharedPreferences(
            "Setting",
            Context.MODE_PRIVATE
        );

        val ipAdd= sharedPref.getString("IpAddress", null)
        val pub = sharedPref.getString("PubName", null)
        val port = sharedPref.getString("Port", null)
        val empId = sharedPref.getString("empId",null)

        //Toast.makeText(context,"http://$ipAdd:$port/$pub/MobileUser/",Toast.LENGTH_SHORT).show()
       val btnsendMessage : TextView = view?.findViewById(R.id.btn_send_Message)


        val compositeDisposable = CompositeDisposable()
        var iMyAPI: IMyAPI = RetrofitClient.getInstance(ipAdd, pub,port).create(IMyAPI::class.java)



        btnsendMessage.setOnClickListener {
            val mess  = view?.findViewById<EditText>(R.id.edt_message_txt_s)
            val manager = requireActivity().supportFragmentManager

            //  Toast.makeText(context,"message:"+ messages.message + "affair "+messages.affairId +"empid "+messages.employeeId,Toast.LENGTH_LONG).show()
            val editMessage = mess.text.toString();
            val messages = Messagess(editMessage,affairId,empId);
            val dialog = SpotsDialog.Builder().setContext(this.requireContext()).build()
            dialog.show()
            compositeDisposable.addAll(
                iMyAPI.sendMessage(messages)
                    .subscribeOn(Schedulers.io())
                    .observeOn(AndroidSchedulers.mainThread())
                    .subscribe(
                        { s ->
                           Toast.makeText(this.requireContext(),s, Toast.LENGTH_SHORT).show()
                            dialog.dismiss()
                            manager.beginTransaction().remove(this).commit()
                            val intent =
                                Intent(view.context, HomeScreen::class.java)

                            val user = TblUser(empId)
                            intent.putExtra("userViewModel", user);
                            view.context.startActivity(intent)
                        }
                       ,
                        { t: Throwable? ->
                            Toast.makeText(this.requireContext(), t!!.message, Toast.LENGTH_SHORT)
                                .show()
                            dialog.dismiss()
                            manager.beginTransaction().remove(this).commit()

                        }


                    ))



        }



    }


}