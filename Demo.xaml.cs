using Newtonsoft.Json;
using SocialBladeDashboard.Helper;
using SocialBladeDashboard.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Controls;

namespace SocialBladeDashboard
{
    /// <summary>
    /// Interaction logic for Demo.xaml
    /// </summary>
    public partial class Demo : UserControl
    {
        readonly HttpClient httpClient = new();
        public Demo()
        {
            httpClient.BaseAddress = new Uri("https://localhost:44362/api/test/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );
            InitializeComponent();
        }

        private void Load_Members(object sender, RoutedEventArgs e)
        {
            GetMembers();
            UpdateFormValue(null, true);
        } 

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var member = new Members
                {
                    Id = Convert.ToInt32(txtMemberId.Text),
                    Name = txtName.Text,
                    Age = Convert.ToInt32(txtAge.Text),
                    Mobile = txtMobile.Text,
                };
                UpdateMember(member);
            }
            catch {
                ErrMsg.Text = "Failed! Something went wrong!";
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext is Members members)
            {
                UpdateFormValue(members,false);
            }
        }

        private void Visibility_Check(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                // Find the parent row
                DataGridRow row = VisualTreeHelpers.FindAncestor<DataGridRow>(button);
                if (row != null && row.Item == dgMembers.Items[dgMembers.Items.Count - 1])
                {
                    button.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void UpdateFormValue(Members members,bool IsClear)
        {
            if (IsClear)
            {
                txtMemberId.Text = "0";
                txtAge.Text = "";
                txtName.Text = "";
                txtMobile.Text = "";
                ErrMsg.Text = "";
                return;
            }
            txtMemberId.Text = members.Id.ToString();
            txtAge.Text = members.Age.ToString();
            txtName.Text = members.Name;
            txtMobile.Text = members.Mobile;
        }

        private async void GetMembers()
        {
            var response = await httpClient.GetStringAsync("Get");
            var data = JsonConvert.DeserializeObject<List<Members>>(response);
            dgMembers.DataContext = data;
        }

        private async void UpdateMember(Members members)
        {
            var currentMember = await GetSingleMember(members.Id);

            if(currentMember is null)
            {
                UpdateFormValue(members, true);
                ErrMsg.Text = "Sorry! Member not found!";
                return;
            }
            currentMember.Name = members.Name;
            currentMember.Age = members.Age;
            currentMember.Mobile = members.Mobile;
            currentMember.ModifiedBy = "API";
            currentMember.ModifiedDate = DateTime.Now;
            await httpClient.PutAsJsonAsync("UpdateMember/" + currentMember.Id, currentMember);
            UpdateFormValue(members, true);
            
        }

        private async Task<Members> GetSingleMember(int memberId)
        {
            var response = await httpClient.GetStringAsync("Get/" + memberId);
            var data = JsonConvert.DeserializeObject<Members>(response);

            return data;
        }
    }
}
