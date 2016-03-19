using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shell;

namespace HotspotMe
{
    public class JumpThings
    {
        public JumpList creatList()
        {

                //IconResourcePath = Assembly.GetEntryAssembly().CodeBase,
                //ApplicationPath = Assembly.GetEntryAssembly().CodeBase
          
            JumpList jumpList = new JumpList();
            jumpList.JumpItems.Add(buildTask("Start Hotspot","","Starts Hotspot with saved parameters","Schnellzugriff", ""));
            jumpList.JumpItems.Add(buildTask("Stop Hotspot", "", "Stops Hotspot", "Schnellzugriff", ""));
            jumpList.JumpItems.Add(buildTask("Open App", "", "Opens the UserInterface", "GUI", ""));

            jumpList.ShowFrequentCategory = false;
            jumpList.ShowRecentCategory = false;
            return jumpList;


        }

        private JumpTask buildTask(string _title, string _arguments, string _description, string _customcategory, string _workingdirectory)
        {
            JumpTask _jTask = new JumpTask();
            _jTask.Title = _title;
            _jTask.Arguments = _arguments;
            
            _jTask.Description = _description;
            _jTask.CustomCategory = _customcategory;
            _jTask.ApplicationPath = Assembly.GetEntryAssembly().CodeBase;
            _jTask.IconResourcePath = Assembly.GetEntryAssembly().CodeBase;            
            _jTask.WorkingDirectory = _workingdirectory;

            return _jTask;
        }

    }
}
