namespace OnlineShop.Models
{
    public class SpecialTagValid : ISpecialTagValid
    {
        public SpecialTagValid() { }
        public bool CheckSpecialTagName(SpecialTags specialTags)
        {
            if(specialTags.SpecialTag.Length== 0 || specialTags.SpecialTag.Trim(' ').Length== 0)
            return false;
            else return true;
        }
    }
}
