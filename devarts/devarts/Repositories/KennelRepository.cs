using devarts.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace devarts.Repositories
{
    public class KennelRepository : IKennelRepository
    {

        private SiteDbContext _db;
        private KennelDbContext _kennelDb;

        public KennelRepository()
        {
            _db = new SiteDbContext();
            _kennelDb = new KennelDbContext();
        }

        /// RASY        
        public DogBreed GetDogBreedById(int id)
        {
            return _kennelDb.DogBreeds.FirstOrDefault(b => b.Id == id);
        }

        public DogBreed GetBreedByBreedLink(string breedLink)
        {
            return _kennelDb.DogBreeds.FirstOrDefault(p => p.BreedLink.ToLower() == breedLink.ToLower());
        }

        public IQueryable<DogBreed> GetAllDogBreed()
        {
            return _kennelDb.DogBreeds;
        }

        public void AddDogBreed(DogBreed item)
        {
            _kennelDb.DogBreeds.Add(item);            
        }

        public void RemoveDogBreed(DogBreed item)
        {
            _kennelDb.DogBreeds.Remove(item);
            _kennelDb.SaveChanges();
        }

        /// PSY        
        public Dog GetDogById(int id)
        {
            return _kennelDb.Dogs.FirstOrDefault(b => b.Id == id);
        }

        public Dog GetDogByDogLink(string dogLink)
        {
            return _kennelDb.Dogs.FirstOrDefault(p => p.DogLink.ToLower() == dogLink.ToLower());
        }

        public IQueryable<Dog> GetAllDogs()
        {
            return _kennelDb.Dogs;
        }

        public Dog GetDogByLitterId(int id)
        {
            return _kennelDb.Dogs.FirstOrDefault(b => b.Id == id);
        }

        public void AddDog(Dog item)
        {
            _kennelDb.Dogs.Add(item);
        }

        public void RemoveDog(Dog item)
        {
            _kennelDb.Dogs.Remove(item);
            _kennelDb.SaveChanges();
        }

        /// ZDJĘCIA PSÓW
        public DogImages GetDogImageById(int id)
        {
            return _kennelDb.DogImages.FirstOrDefault(f => f.Id == id);
        }

        public DogImages GetDogImageFileByImageFileNameAndDog(string imgFileName, string dogLink)
        {
            return _kennelDb.DogImages.FirstOrDefault(i => i.ImageFileName.ToLower() == imgFileName.ToLower() && i.DogLink.ToLower() == dogLink.ToLower());
        }

        public DogImages GetDogByImageFile(string imageFileName)
        {
            return _kennelDb.DogImages.FirstOrDefault(f => f.ImageFileName == imageFileName);
        }

        public List<DogImages> GetAllDogImages()
        {
            return _kennelDb.DogImages.OrderBy(u => u.DogId).ToList();
        }

        public DogImages GetMainImageOfDog(string doglink)
        {
            return _kennelDb.DogImages.OrderBy(u => u.Id).FirstOrDefault(u => u.DogLink == doglink);
        }
        
        public List<DogImages> GetImagesByDogId(int dogId)
        {
            return _kennelDb.DogImages.Where(p => p.DogId == dogId).ToList();
        }

        public List<DogImages> GetImagesByDogLink(string dogLink)
        {
            return _kennelDb.DogImages.Where(p => p.DogLink == dogLink).OrderBy(i => i.ImageIndex).ToList();
        }

        public void AddDogImage(DogImages DogImage)
        {
            _kennelDb.DogImages.Add(DogImage);
        }

        public void DeleteImage(DogImages DogImage)
        {
            _kennelDb.DogImages.Remove(DogImage);
            _kennelDb.SaveChanges();
        }

        /// MIOTY
        public IQueryable<Litter> GetAllLitters()
        {
            return _kennelDb.Litters.OrderByDescending(l => l.BornDateOkay);
        }

        public Litter GetLitterById(int id)
        {
            return _kennelDb.Litters.FirstOrDefault(litter => litter.Id == id);
        }

        public Litter GetLitterByLitterLink(string litterLink)
        {
            return _kennelDb.Litters.FirstOrDefault(p => p.LitterLink.ToLower() == litterLink.ToLower());
        }

        public IQueryable<Litter> GetLittersByDogId(int id)
        {
            return _kennelDb.Litters.Where(litter => litter.DogId == id);
        }

        public IQueryable<Litter> GetLittersByDogLink(string dogLink)
        {
            return _kennelDb.Litters.Where(litter => litter.DogLink.ToLower() == dogLink.ToLower());
        }

        public LitterImages GetLitterImageFileByImageFileNameAndLitter(string imgFileName, string litterLink)
        {
            return _kennelDb.LitterImages.FirstOrDefault(i => i.ImageFileName.ToLower() == imgFileName.ToLower() && i.LitterLink.ToLower() == litterLink.ToLower());
        }

        public void AddLitter(Litter litter)
        {
            _kennelDb.Litters.Add(litter);
        }

        public void RemoveLitter(Litter litter)
        {
            _kennelDb.Litters.Remove(litter);
        }

        /// ZDJĘCIA ZAPOWIEDZI W MIOTACH
        public LitterImages GetLitterImageById(int id)
        {
            return _kennelDb.LitterImages.FirstOrDefault(litterImg => litterImg.Id == id);
        }

        public LitterImages GetLitterImageByLitterId(int id)
        {
            return _kennelDb.LitterImages.FirstOrDefault(litterImg => litterImg.LitterId == id);
        }

        public LitterImages GetLitterImageByLitterLink(string litterLink)
        {
            return _kennelDb.LitterImages.FirstOrDefault(litterImg => litterImg.LitterLink.ToLower() == litterLink.ToLower());
        }

        public List<LitterImages> GetLitterImagesByLitterLink(string litterLink)
        {
            return _kennelDb.LitterImages.Where(litterImg => litterImg.LitterLink.ToLower() == litterLink.ToLower()).ToList();
        }

        public void AddLitterImage(LitterImages litter)
        {
            _kennelDb.LitterImages.Add(litter);
        }

        public void RemoveLitterImage(LitterImages litter)
        {
            _kennelDb.LitterImages.Remove(litter);
        }

        /// SZCZENIĘTA
        public IQueryable<Puppy> GetAllPuppies()
        {
            return _kennelDb.Puppies;
        }

        public Puppy GetPuppyById(int id)
        {
            return _kennelDb.Puppies.FirstOrDefault(puppy => puppy.Id == id);
        }

        public Puppy GetPuppyByPuppyLink(string puppyLink)
        {
            return _kennelDb.Puppies.FirstOrDefault(puppy => puppy.PuppyLink.ToLower() == puppyLink.ToLower());
        }

        public Puppy GetPuppyByBreedLinkAndPuppyLink(string breedLink, string puppyLink)
        {
            return _kennelDb.Puppies.FirstOrDefault(puppy => puppy.BreedLink.ToLower() == breedLink.ToLower()
            && puppy.PuppyLink.ToLower() == puppyLink.ToLower());
        }

        public IQueryable<Puppy> GetPuppiesByLitterId(int id)
        {
            return _kennelDb.Puppies.Where(puppies => puppies.LitterId == id);
        }

        public IQueryable<Puppy> GetPuppiesByLitterLink(string litterLink)
        {
            return _kennelDb.Puppies.Where(puppies => puppies.LitterLink == litterLink);
        }

        public void AddPuppy(Puppy puppy)
        {
            _kennelDb.Puppies.Add(puppy);
        }

        public void RemovePuppy(Puppy puppy)
        {
            _kennelDb.Puppies.Remove(puppy);
        }

        /// ZDJĘCIA SZCZENIĄT
        public PuppyImages GetPuppyImageById(int id)
        {
            return _kennelDb.PuppyImages.FirstOrDefault(puppy => puppy.Id == id);
        }

        public IQueryable<PuppyImages> GetImagesByPuppyId(int id)
        {
            return _kennelDb.PuppyImages.Where(puppies => puppies.LitterId == id);
        }

        public IQueryable<PuppyImages> GetImagesByPuppyLink(string puppyLink)
        {
            return _kennelDb.PuppyImages.Where(puppies => puppies.PuppyLink == puppyLink).OrderBy(i => i.ImageIndex);
        }

        public List<PuppyImages> GetPuppyImagesByLitterLink(string litterLink)
        {
            return _kennelDb.PuppyImages.Where(puppies => puppies.LitterLink == litterLink).ToList();
        }

        public List<PuppyImages> GetPuppyImagesByLitterLinkAndPuppyLink(string litterLink, string puppyLink)
        {
            return _kennelDb.PuppyImages.Where(puppies => puppies.LitterLink.ToLower() == litterLink.ToLower() &&
            puppies.PuppyLink.ToLower() == puppyLink.ToLower()).ToList();
        }

        public List<PuppyImages> GetPuppyImagesByBreedLinkLitterLinkAndPuppyLink(string breedLink, string litterLink, string puppyLink)
        {
            return _kennelDb.PuppyImages.Where(puppies => puppies.BreedLink.ToLower() == breedLink.ToLower() && puppies.LitterLink.ToLower() == litterLink.ToLower() &&
            puppies.PuppyLink.ToLower() == puppyLink.ToLower()).ToList();
        }

        public PuppyImages GetPuppyImageByBreedLinkLitterLinkAndPuppyLink(string imgFileName, string breedLink, string litterLink, string puppyLink)
        {
            return _kennelDb.PuppyImages.FirstOrDefault(puppies => puppies.BreedLink.ToLower() == breedLink.ToLower() && puppies.LitterLink.ToLower() == litterLink.ToLower() &&
            puppies.PuppyLink.ToLower() == puppyLink.ToLower() && puppies.ImageFileName.ToLower() == imgFileName.ToLower());
        }

        public void AddPuppyImage(PuppyImages puppy)
        {
            _kennelDb.PuppyImages.Add(puppy);
        }

        public void RemovePuppyImage(PuppyImages puppy)
        {
            _kennelDb.PuppyImages.Remove(puppy);
        }

        /// DRZEWA GENEALOGICZNE
        public IQueryable<Tree> GetTrees()
        {
            return _kennelDb.Trees;
        }

        public Tree GetTreeById(int id)
        {
            return _kennelDb.Trees.FirstOrDefault(t => t.Id == id);
        }

        // drzewo genealogiczne psa
        public Tree GetDogTreeByDogLink(string dogLink)
        {
            return _kennelDb.Trees.Where(t => t.IsLitter == false).FirstOrDefault(t => t.DogLink.ToLower() == dogLink.ToLower());
        }

        // drzewo genealogiczne miotu
        public Tree GetLitterTreeByLitterLink(string litterLink)
        {
            return _kennelDb.Trees.FirstOrDefault(t => t.LitterLink.ToLower() == litterLink.ToLower());
        }

        public void DropAndCreateGenealogicTreesTable()
        {
            // ExecuteSqlCommand działa z pojedyńczym STATEMENTEM !!!
            using (var db = new KennelDbContext())
            {
                db.Database.ExecuteSqlCommand("DROP TABLE [Trees];");
                string query = 
                            "CREATE TABLE [Trees]" +
                            "(" +
                            "   [Id] INT NOT NULL IDENTITY (1,1)," +
                            "   [DogId] INT," +
                            "   [LitterId] INT," +
                            "   [DogLink] NVARCHAR(4000)," +
                            "   [LitterLink] NVARCHAR(4000)," +
                            "   [CreateDate] DATETIME," +
                            "   [EditDate] DATETIME," +
                            "   " +
                            "   [DogTreeBox] NVARCHAR(4000)," +
                            "   [DogTreeTooltip] NVARCHAR(4000)," +
                            "   [DogTreeTooltip_Image] NVARCHAR(4000)," +
                            "   [A_DogTreeBox_Father_1] NVARCHAR(4000)," +
                            "   [A_DogTreeTooltip_Father_1] NVARCHAR(4000)," +
                            "   [A_DogTreeImage_Father_1] NVARCHAR(4000)," +
                            "   [A_DogTreeBox_Mother_2] NVARCHAR(4000)," +
                            "   [A_DogTreeTooltip_Mother_2] NVARCHAR(4000)," +
                            "   [A_DogTreeImage_Mother_2] NVARCHAR(4000)," +
                            "   " +
                            "   [B_DogTreeBox_Father_3] NVARCHAR(4000)," +
                            "   [B_DogTreeTooltip_Father_3] NVARCHAR(4000)," +
                            "   [B_DogTreeImage_Father_3] NVARCHAR(4000)," +
                            "   [B_DogTreeBox_Mother_4] NVARCHAR(4000)," +
                            "   [B_DogTreeTooltip_Mother_4] NVARCHAR(4000)," +
                            "   [B_DogTreeImage_Mother_4] NVARCHAR(4000)," +
                            "   [B_DogTreeBox_Father_5] NVARCHAR(4000)," +
                            "   [B_DogTreeTooltip_Father_5] NVARCHAR(4000)," +
                            "   [B_DogTreeImage_Father_5] NVARCHAR(4000)," +
                            "   [B_DogTreeBox_Mother_6] NVARCHAR(4000)," +
                            "   [B_DogTreeTooltip_Mother_6] NVARCHAR(4000)," +
                            "   [B_DogTreeImage_Mother_6] NVARCHAR(4000)," +
                            "   [C_DogTreeBox_Father_7] NVARCHAR(4000)," +
                            "   [C_DogTreeTooltip_Father_7] NVARCHAR(4000)," +
                            "   [C_DogTreeImage_Father_7] NVARCHAR(4000)," +
                            "   [C_DogTreeBox_Mother_8] NVARCHAR(4000)," +
                            "   [C_DogTreeTooltip_Mother_8] NVARCHAR(4000)," +
                            "   [C_DogTreeImage_Mother_8] NVARCHAR(4000)," +
                            "   [C_DogTreeBox_Father_9] NVARCHAR(4000)," +
                            "   [C_DogTreeTooltip_Father_9] NVARCHAR(4000)," +
                            "   [C_DogTreeImage_Father_9] NVARCHAR(4000)," +
                            "   [C_DogTreeBox_Mother_10] NVARCHAR(4000)," +
                            "   [C_DogTreeTooltip_Mother_10] NVARCHAR(4000)," +
                            "   [C_DogTreeImage_Mother_10] NVARCHAR(4000)," +
                            "   [C_DogTreeBox_Father_11] NVARCHAR(4000)," +
                            "   [C_DogTreeTooltip_Father_11] NVARCHAR(4000)," +
                            "   [C_DogTreeImage_Father_11] NVARCHAR(4000)," +
                            "   [C_DogTreeBox_Mother_12] NVARCHAR(4000)," +
                            "   [C_DogTreeTooltip_Mother_12] NVARCHAR(4000)," +
                            "   [C_DogTreeImage_Mother_12] NVARCHAR(4000)," +
                            "   [C_DogTreeBox_Father_13] NVARCHAR(4000)," +
                            "   [C_DogTreeTooltip_Father_13] NVARCHAR(4000)," +
                            "   [C_DogTreeImage_Father_13] NVARCHAR(4000)," +
                            "   [C_DogTreeBox_Mother_14] NVARCHAR(4000)," +
                            "   [C_DogTreeTooltip_Mother_14] NVARCHAR(4000)," +
                            "   [C_DogTreeImage_Mother_14] NVARCHAR(4000)," +
                            "   [GenerationsCount] INT DEFAULT 3," +
                            "   [Visible] BIT DEFAULT 0," +
                            "   [IsLitter] BIT DEFAULT 0" +
                            "); ";
                                           
                db.Database.ExecuteSqlCommand(query);
                db.Database.ExecuteSqlCommand("ALTER TABLE [Trees] ADD CONSTRAINT [PK_dbo.Trees] PRIMARY KEY ([Id]);");
                db.Database.ExecuteSqlCommand("CREATE INDEX [Idx_Id] ON [Trees] ([Id] ASC);");
                db.Database.ExecuteSqlCommand("CREATE INDEX [Idx_DogLink] ON [Trees] ([DogLink] ASC);");
            }           
        }

        public void AddTree(Tree tree)
        {
            _kennelDb.Trees.Add(tree);
        }

        public void RemoveTree(Tree tree)
        {
            _kennelDb.Trees.Remove(tree);
        }

        /// DOKUMENTY HODOWLI
        public IQueryable<Document> GetDocumetsList()
        {
            return _kennelDb.Documents;
        }

        public IQueryable<Reproduction> GetBitchesList()
        {
            return _kennelDb.Reproduction;
        }

        public IQueryable<PuppieOfBitch> GetAllPuppiesList()
        {
            return _kennelDb.PuppieOfBitch;
        }

        public IQueryable<PuppieOfBitch> GetPuppiesByBitchList(int bitchId)
        {
            return _kennelDb.PuppieOfBitch;
        }

        public void SaveChanges()
        {
            _kennelDb.SaveChanges();
        }
    }
}