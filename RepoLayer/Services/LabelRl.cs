using Microsoft.Extensions.Configuration;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepoLayer.Services
{
    public class LabelRl : ILabelRl
    {
        fundooContext fundooContext;
        private readonly IConfiguration config;
        public LabelRl(fundooContext fundooContext, IConfiguration config)
        {
            this.config = config;
            this.fundooContext = fundooContext;
        }
        public LabelEntity Addlabel(long noteid, long userid, string label)
        {
            try
            {
                LabelEntity Entity = new LabelEntity();
                Entity.LabelName = label;
                Entity.Userid = userid;
                Entity.Noteid = noteid;
                this.fundooContext.Labels.Add(Entity);
                int result = this.fundooContext.SaveChanges();
                if (result > 0)
                {
                    return Entity;
                }
                return null;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<LabelEntity> GetlabelsByNoteid(long noteid, long userid)
        {
            return fundooContext.Labels.Where(e => e.Noteid == noteid && e.Userid == userid).ToList();
        }
        public bool RemoveLabel(long userID, string labelName)
        {
            try
            {
                var result = this.fundooContext.Labels.FirstOrDefault(x => x.Userid == userID && x.LabelName == labelName);
                if (result != null)
                {
                    fundooContext.Remove(result);
                    fundooContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<LabelEntity> RenameLabel(long userID, string oldLabelName, string labelName)
        {
            IEnumerable<LabelEntity> labels;
            labels = fundooContext.Labels.Where(x => x.Userid == userID && x.LabelName == oldLabelName).ToList();
            if (labels != null)
            {
                foreach (var newlabel in labels)
                {
                    newlabel.LabelName = labelName;
                }
                fundooContext.SaveChanges();
                return labels;
            }
            return null;
        }
    }
}
