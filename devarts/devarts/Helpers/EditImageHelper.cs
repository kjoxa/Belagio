//using devarts.Repositories;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Web;
//using System.Web.Hosting;

//namespace devarts.Helpers
//{    
//    public class EditImage
//    {
//        AdminRepository _adminRepo;
//        //LittersRepository _litterRepo;
//        PostRepository _postRepo;

//        public EditImage()
//        {
//            _adminRepo = new AdminRepository();
//            //_litterRepo = new LittersRepository();
//            _postRepo = new PostRepository();
//        }

//        /*Edytowanie zdjęcia w newsach - ImgFiles*/
//        public void RenameImgFilesFileName(string oldFilePath, string newFileName, bool removeOriginal)
//        {
//            var renameRecord = _postRepo.GetPostByImageFile(Path.GetFileName(oldFilePath));

//            renameRecord.ImageFileName = Path.GetFileName(newFileName);
//            _adminRepo.SaveChanges();

//            if (removeOriginal)
//            {
//                File.Delete(oldFilePath);
//            }
//        }

//        /*Edytowanie zdjęcia głównego miotu - Litters*/
//        public void RenameLittersFileName(string oldFilePath, string newFileName, bool removeOriginal)
//        {
//            var renameRecord = _litterRepo.GetLittersByImageFile(Path.GetFileName(oldFilePath));

//            renameRecord.ImgFileName = Path.GetFileName(newFileName);
//            _litterRepo.SaveChanges();

//            if (removeOriginal)
//            {
//                File.Delete(oldFilePath);
//            }
//        }

//        /*Edytowanie zdjęcia wewnątrz Litters - DogByLitters */
//        public void RenameDogByLittersFileName(string oldFilePath, string newFileName, bool removeOriginal)
//        {
//            var renameRecord = _litterRepo.GetDogByImageFile(Path.GetFileName(oldFilePath));

//            renameRecord.ImgFileName = Path.GetFileName(newFileName);
//            _litterRepo.SaveChanges();

//            if (removeOriginal)
//            {
//                File.Delete(oldFilePath);
//            }
//        }

//        /*Edytowanie zdjęcia wewnątrz poszczególnych psów w miocie - ImagesForLitterDog */
//        public void RenameImagesForLitterDogFileName(string oldFilePath, string newFileName, bool removeOriginal)
//        {
//            var renameRecord = _litterRepo.GetLitterDogByImageFile(Path.GetFileName(oldFilePath));

//            renameRecord.ImgFileName = Path.GetFileName(newFileName);
//            _litterRepo.SaveChanges();

//            if (removeOriginal)
//            {
//                File.Delete(oldFilePath);
//            }
//        }
//    }
//}